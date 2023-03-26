using System;
using Toolbox.NFC.Enums;
using Toolbox.NFC.WinSCard;
using Toolbox.NFC.Reader.Driver;
using Toolbox.NFC.Reader.Apdu;
using SCard = Toolbox.NFC.WinSCard.WinSCardInterop;

namespace Toolbox.NFC.Card
{
    public sealed class SmartCard : IDisposable
    {
        private readonly IntPtr _hContext = IntPtr.Zero;
        private IntPtr _hCard = IntPtr.Zero;
        private readonly string _readerName;
        private IDriver _driver = null;

        public SmartCard(ref IntPtr ReaderContext, string readerName)
        {
            _hContext = ReaderContext;
            _readerName = readerName;   
        }

        /// <summary>
        /// Execute APDU command
        /// </summary>
        /// <param name="command">Command to exctute</param>
        /// <returns>ApduResponse</returns>
        /// <exception cref="Exception">No card or unsupported reader attached</exception>
        internal ApduResponse Execute(ApduCommand command, int txLen = -1)
        {
            if (_hCard == IntPtr.Zero)
                throw new Exception("No card attached");
            AttachDriver();
            return _driver.Execute(command, txLen);
        }

        internal ApduResponse Execute(byte[] cmd, int txLen = -1)
        {
            if (_hCard == IntPtr.Zero)
                throw new Exception("No card attached");
            AttachDriver();
            return _driver.Execute(cmd, txLen);
        }

        /// <summary>
        /// Attach driver internal driver object
        /// </summary>
        private void AttachDriver()
        {
            _driver = DriverHandler.GetDriver(GetReaderType(), _hCard);
        }

        /// <summary>
        /// Get card connected state
        /// </summary>
        /// <returns>boolean</returns>
        public bool IsCardConnected()
        {
            if (_hContext == IntPtr.Zero) return false;
            IntPtr protocol = IntPtr.Zero;
            
            var retval = SCard.SCardConnect(
                _hContext, _readerName,
                SCard.SCARD_SHARE_SHARED,
                SCardProtocol.SCARD_PROTOCOL_Tx,
                ref _hCard,
                ref protocol);
            return retval == ErrorCode.SCARD_S_SUCCESS;
        }

        /// <summary>
        /// Disconnect card
        /// </summary>
        public void DisconnectCard()
        {
            if (_hCard == IntPtr.Zero) return;
            _ = SCard.SCardDisconnect(_hCard, 0);
            
            _hCard = IntPtr.Zero;
        }

        /// <summary>
        /// Get connected card type if card is connected
        /// </summary>
        /// <returns>CardType</returns>
        public CardType GetConnectedCardType()
        {
            if (!IsCardConnected()) return CardType.Unknown_Card_Type;

            SCard_ReaderState readerState = new SCard_ReaderState()
            {
                RdrName = _readerName,
                RdrCurrState = SCard.SCARD_STATE_UNAWARE,
                RdrEventState = SCard.SCARD_STATE_UNAWARE,
                UserData = "Mifare Card"
            };

            uint readerCount = 1;
            var ret = SCard.SCardGetStatusChange(_hContext, 100, ref readerState, readerCount);
            if (ret != ErrorCode.SCARD_S_SUCCESS) return CardType.Unknown_Card_Type;
            if (readerState.ATRLength < 6) return CardType.Unknown_Card_Type;

            return DriverHandler.GetCardType(GetReaderType(), (int)readerState.ATRValue[readerState.ATRLength - 6]);
        }
        
        /// <summary>
        /// Get Card UID
        /// </summary>
        /// <returns>UID</returns>
        public byte[] GetUID()
        {
            var apduCommand = Commands.MifareUidCommand.Get(GetReaderType());
            var response = Execute(apduCommand);
            if (!response.Success) return null;
            return response.ResponseData;
        }
        /// <summary>
        /// Get attached reader type
        /// </summary>
        /// <returns>ReaderType</returns>
        public ReaderType GetReaderType() => DriverHandler.GetReaderType(_readerName);
        
        public void Dispose()
        {
            DisconnectCard();
        }
    }
}
