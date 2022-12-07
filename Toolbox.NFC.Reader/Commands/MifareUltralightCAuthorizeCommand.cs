using Toolbox.NFC.Reader.Apdu;
using Toolbox.NFC.Reader.Driver;

namespace Toolbox.NFC.Reader.Commands
{
    public sealed class MifareUltralightCAuthorizeCommand : ApduCommand
    {
        private readonly byte[] _authKey;
        private readonly ReaderType _readerType;

        public MifareUltralightCAuthorizeCommand(byte[] authKey, ReaderType readerType)
        {
            _authKey = authKey;
            _readerType = readerType;
            if (_readerType == ReaderType.Unsupported)
                throw new Exception("Unsupported reader");
        }

        public override byte[] GetBuffer()
        {
            return _readerType switch
            {
                ReaderType.Omnikey => new Driver.Omnikey.MifareUltralightCAuthorizeCommand(_authKey).GetBuffer(),
                ReaderType.ACR => new Driver.ACR.MifareUltralightCAuthorizeCommand(_authKey).GetBuffer(),
                _ => throw new Exception("Unsupported reader"),
            };
        }

    }
}
