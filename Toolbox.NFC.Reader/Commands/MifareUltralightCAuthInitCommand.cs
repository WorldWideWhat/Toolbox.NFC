using Toolbox.NFC.Reader.Apdu;
using Toolbox.NFC.Reader.Driver;

namespace Toolbox.NFC.Reader.Commands
{
    public sealed class MifareUltralightCAuthInitCommand : ApduCommand
    {
        private readonly ReaderType _readerType;

        public MifareUltralightCAuthInitCommand(ReaderType reader)
        {
            _readerType = reader;
            if (_readerType == ReaderType.Unsupported)
                throw new Exception("Unsupported reader");
        }

        public override byte[] GetBuffer()
        {
            return _readerType switch
            {
                ReaderType.Omnikey => new Driver.Omnikey.MifareUltralightCAuthInitCommand().GetBuffer(),
                ReaderType.ACR => new Driver.ACR.MifareUltralightCAuthInitCommand().GetBuffer(),
                _ => throw new Exception("Unsupported reader"),
            };
        }
    }
}
