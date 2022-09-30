using System.Text;
using Toobox.NFC.WinSCard;
using WinSCard = Toobox.NFC.WinSCard.WinSCardInterop;

namespace Toolbox.NFC.Reader
{
    public sealed class Reader : IDisposable
    {

        private IntPtr _hContext = IntPtr.Zero;
        private string? _readerName;
        private SmartCard? _smartCard = null;
        public static List<string> Readers
        {
            get
            {
                List<string> readerList = new();
                IntPtr hContext = IntPtr.Zero;
                var retVal = WinSCard.SCardEstablishContext(2, IntPtr.Zero, IntPtr.Zero, out hContext);
                if (retVal != 0) return readerList;

                uint pcchReaders = 0;
                retVal = WinSCard.SCardListReaders(hContext, null, null, ref pcchReaders);
                if (retVal != ErrorCode.SCARD_S_SUCCESS) return readerList;
                char nullchar = (char)0;

                var mszReaders = new byte[pcchReaders];
                _ = WinSCard.SCardListReaders(hContext, null, mszReaders, ref pcchReaders);
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

                _ = WinSCard.SCardReleaseContext(hContext);
                return readerList;
            }
        }

        public Reader(string readerName)
        {
            _readerName = readerName;
            if(!ConnectReader())
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
            if(!IsReaderConnected() ||_smartCard == null)
            {
                throw new Exception("No reader attached");
            }

            return _smartCard;
        }

        /// <summary>
        /// Establish reader connection
        /// </summary>
        /// <returns></returns>
        public bool ConnectReader()
        {
            var result = WinSCard.SCardEstablishContext(2, IntPtr.Zero, IntPtr.Zero, out _hContext) == ErrorCode.SCARD_S_SUCCESS;
            if (result)
                _smartCard = new SmartCard(ref _hContext, _readerName);
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

            var found =  (readers.Any(x => x.Equals(_readerName)));
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
            _ = WinSCard.SCardReleaseContext(_hContext);
            _hContext = IntPtr.Zero;
        }


        public void Dispose()
        {
            DisconnectReader();
        }
    }
}
