using Toolbox.NFC.WinSCard;
using Toolbox.NFC.Reader.Card;
using Toolbox.NFC.Reader.Driver.ACR;
using Toolbox.NFC.Reader.Driver.Omnikey;
using Toolbox.NFC.Reader.Driver;
using WinSCard = Toolbox.NFC.WinSCard.WinSCardInterop;
using Toolbox.NFC.Reader.Apdu;

namespace Toolbox.NFC.Reader
{
    public sealed class SmartCard : IDisposable
    {
        private IntPtr _hContext = IntPtr.Zero;
        private IntPtr _hCard = IntPtr.Zero;
        private readonly string _readerName;
        private IDriver? _driver = null;


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
        public ApduResponse Execute(ApduCommand command)
        {
            if (_hCard == IntPtr.Zero)
                throw new Exception("No card attached");
            AttachDriver();
            return _driver!.Execute(command);
        }

        private void AttachDriver()
        {
            _driver = GetReaderType() switch
            {
                ReaderType.Omnikey => new OmnikeyDriver(_hCard),
                ReaderType.ACR => new AcrDriver(_hCard),
                _ => throw new Exception("Unsupported reader type"),
            };
        }

        /// <summary>
        /// Get card connected state
        /// </summary>
        /// <returns>boolean</returns>
        public bool IsCardConnected()
        {
            if (_hContext == IntPtr.Zero) return false;
            IntPtr protocol = IntPtr.Zero;
            var retval = WinSCard.SCardConnect(
                _hContext, _readerName,
                WinSCard.SCARD_SHARE_SHARED,
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
            _ = WinSCard.SCardDisconnect(_hCard, 0);
            
            _hCard = IntPtr.Zero;
        }

        /// <summary>
        /// Get connected card type if card is connected
        /// </summary>
        /// <returns>CardType</returns>
        public CardType GetConnectedCardType()
        {
            if (!IsCardConnected()) return CardType.Unknown_Card_Type;

            SCard_ReaderState readerState = new()
            {
                RdrName = _readerName,
                RdrCurrState = WinSCard.SCARD_STATE_UNAWARE,
                RdrEventState = WinSCard.SCARD_STATE_UNAWARE,
                UserData = "Mifare Card"
            };

            uint readerCount = 1;
            var ret = WinSCard.SCardGetStatusChange(_hContext, 100, ref readerState, readerCount);
            if (ret != ErrorCode.SCARD_S_SUCCESS) return CardType.Unknown_Card_Type;
            if (readerState.ATRLength < 6) return CardType.Unknown_Card_Type;

            return DriverHandler.GetReaderType(_readerName) switch
            {
                ReaderType.Omnikey => OmnikeyDriver.GetCardType((int)readerState.ATRValue[readerState.ATRLength - 6]),
                ReaderType.ACR => AcrDriver.GetCardType((int)readerState.ATRValue[readerState.ATRLength - 6]),
                _ => CardType.Unknown_Card_Type,
            };
        }
        
        
        public byte[]? GetUID()
        {
            var command = new Commands.GetUidCommand(GetReaderType());
            var response = Execute(command);
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
