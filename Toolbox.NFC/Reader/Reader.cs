using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using Toolbox.NFC.WinSCard;
using Toolbox.NFC.Reader.Event;
using Toolbox.NFC.Tools;
using SCard = Toolbox.NFC.WinSCard.WinSCardInterop;
using Toolbox.NFC.Card;
using System.Linq;

namespace Toolbox.NFC.Reader
{
    public sealed class Reader : IDisposable
    {

        private IntPtr _hContext = IntPtr.Zero;
        private readonly string _readerName;
        private SmartCard _smartCard = null;
        private Task _stateTask = null;
        private CancellationTokenSource _cancelTokenSource;

        private volatile bool _runThread = true;

        public event EventHandler<CardPresentStateArgs> OnCardPresentChangedEvent;
        public event EventHandler<CardInUseStateArgs> OnCardInUseChangedEvent;
        public event EventHandler OnReaderDisconnectedEvent;

        public static List<string> Readers
        {
            get
            {
                var readerList = new List<string>();
                IntPtr hContext = IntPtr.Zero;

                var retVal = SCard.SCardEstablishContext(SCardScope.SCARD_SCOPE_USER, IntPtr.Zero, IntPtr.Zero, out hContext);
                if (retVal != 0) return readerList;

                uint pcchReaders = 0;
                retVal = SCard.SCardListReaders(hContext, null, null, ref pcchReaders);
                if (retVal != ErrorCode.SCARD_S_SUCCESS) return readerList;
                char nullchar = (char)0;

                var mszReaders = new byte[pcchReaders];
                _ = SCard.SCardListReaders(hContext, null, mszReaders, ref pcchReaders);
                var currbuff = Encoding.ASCII.GetString(mszReaders);
                int len = (int)pcchReaders;
                if (len > 0)
                {
                    while (currbuff[0] != nullchar)
                    {
                        var nullindex = currbuff.IndexOf(nullchar);
                        var reader = currbuff.Substring(0, nullindex);
                        readerList.Add(reader);
                        len -= (reader.Length + 1);
                        currbuff = currbuff.Substring(nullindex + 1, len);
                    }
                }

                _ = SCard.SCardReleaseContext(hContext);
                return readerList;
            }
        }

        public Reader(string readerName)
        {
            _readerName = readerName;
            if (!ConnectReader())
            {
                throw new Exception($"Unable to connect to {readerName}");
            }

        }

        /// <summary>
        /// Get smart card object
        /// </summary>
        /// <returns>SmartCard</returns>
        /// <exception cref="Exception">If no readers is attached/connected</exception>
        public SmartCard GetSmartCard()
        {
            if (!IsReaderConnected() || _smartCard == null)
            {
                throw new Exception("No reader attached");
            }
            
            return _smartCard;
        }

        /// <summary>
        /// Raise card present event
        /// </summary>
        /// <param name="present">Card present</param>
        /// <returns>Task</returns>
        private Task RaiseOnCardPresentEvent(bool present)
        {
            if(_cancelTokenSource is null) { _cancelTokenSource = new CancellationTokenSource(); }
            
            if(present)
            {
                if (!_smartCard.IsCardConnected()) present = false;

            }

            return Task.Factory.StartNew(() =>
                {
                    OnCardPresentChangedEvent?.Invoke(this, new CardPresentStateArgs(_readerName, present));
                },
                _cancelTokenSource.Token,
                TaskCreationOptions.LongRunning,
                TaskScheduler.Default);
        }

        /// <summary>
        /// Raise card in use state event
        /// </summary>
        /// <param name="inUse">In use state</param>
        /// <returns>Task</returns>
        private Task RaiseCardInUseStateEvent(bool inUse)
        {
            if (_cancelTokenSource is null) { _cancelTokenSource = new CancellationTokenSource(); }
            return Task.Factory.StartNew(() =>
                {
                    OnCardInUseChangedEvent?.Invoke(this, new CardInUseStateArgs(_readerName, inUse));
                },
                _cancelTokenSource.Token,
                TaskCreationOptions.LongRunning,
                TaskScheduler.Default);
        }

        /// <summary>
        /// Reader state check thread
        /// </summary>
        /// <returns></returns>
        private async Task CheckStateThread()
        {
            if (_readerName is null) return;

            var readerState = new SCard_ReaderState
            {
                RdrName = _readerName
            };

            uint eventState = 0;
            if (_cancelTokenSource is null) { _cancelTokenSource = new CancellationTokenSource(); }
            while (_runThread && !_cancelTokenSource.IsCancellationRequested)
            {
                var result = SCard.SCardGetStatusChange(
                    _hContext,
                    1000,
                    ref readerState,
                    1);

                if (result != ErrorCode.SCARD_S_SUCCESS)
                {
                    _runThread = false;
                    OnReaderDisconnectedEvent?.Invoke(this, EventArgs.Empty);
                    break;
                }

                if (eventState != readerState.RdrEventState)
                {
                    if (BinaryTools.FlagChanged(eventState, readerState.RdrEventState, SCardState.Present))
                    {
                        var present = BinaryTools.IsFlagSet(readerState.RdrEventState, SCardState.Present);
                        await RaiseOnCardPresentEvent(present);
                    }

                    if (BinaryTools.FlagChanged(eventState, readerState.RdrEventState, SCardState.Inuse))
                    {
                        var inuse = BinaryTools.IsFlagSet(readerState.RdrEventState, SCardState.Inuse);
                        await RaiseCardInUseStateEvent(inuse);
                    }
                    eventState = readerState.RdrEventState;
                }
                Thread.Sleep(100);
            }
        }

        /// <summary>
        /// Establish reader connection
        /// </summary>
        /// <returns></returns>
        public bool ConnectReader()
        {
            if (_readerName is null)
                throw new Exception("No reader selected");
            var result = SCard.SCardEstablishContext(
                SCardScope.SCARD_SCOPE_USER,
                IntPtr.Zero,
                IntPtr.Zero,
                out _hContext) == ErrorCode.SCARD_S_SUCCESS;
            if (result)
            {
                _smartCard = new SmartCard(ref _hContext, _readerName);

                if (_stateTask != null)
                {
                    _runThread = false;
                    _cancelTokenSource?.Cancel();
                    _stateTask = null;
                }
                _runThread = true;

                _cancelTokenSource = new CancellationTokenSource();
                _stateTask = Task.Factory.StartNew(async () => await CheckStateThread(),
                    _cancelTokenSource.Token,
                    TaskCreationOptions.LongRunning,
                    TaskScheduler.Default);
            }
            return result;
        }

        /// <summary>
        /// Check if reader is attached
        /// </summary>
        /// <returns></returns>
        public bool IsReaderConnected()
        {
            var readers = Readers;
            if (readers is null || _hContext == IntPtr.Zero) return false;

            var found = (readers.Any(x => x.Equals(_readerName)));
            if (!found) DisconnectReader();
            return found;
        }

        /// <summary>
        /// Disconnect reader
        /// </summary>
        public void DisconnectReader()
        {
            if (_hContext == IntPtr.Zero) return;
            if (_smartCard != null)
            {
                if (_smartCard.IsCardConnected())
                    _smartCard.DisconnectCard();
            }
            _ = SCard.SCardReleaseContext(_hContext);
            _hContext = IntPtr.Zero;
            _runThread = false;
        }

        /// <summary>
        /// Dispose items
        /// </summary>
        public void Dispose()
        {
            DisconnectReader();
        }
    }
}