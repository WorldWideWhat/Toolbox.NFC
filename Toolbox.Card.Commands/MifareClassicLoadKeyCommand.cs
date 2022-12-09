using Toolbox.NFC.Reader.Apdu;
using Toolbox.NFC.Reader.Driver;

namespace Toolbox.NFC.Reader.Commands
{
    internal class MifareClassicLoadKeyCommand : ApduCommand
    {
        private readonly ReaderType _readerType;
        public MifareClassicLoadKeyCommand(byte[] key, ReaderType reader)
        {
            _readerType = reader;
            if (_readerType == ReaderType.Unsupported)
                throw new Exception("Unsupported reader");
            base.CommandData = key;
        }

        public override byte[] GetBuffer()
        {
            return _readerType switch
            {
                ReaderType.Omnikey => new Driver.Omnikey.MifareClassicLoadKeyCommand(base.CommandData!).GetBuffer(),
                ReaderType.ACR => new Driver.ACR.MifareClassicLoadKeyCommand(base.CommandData!).GetBuffer(),
                _ => throw new Exception("Unsupported reader"),
            };
        }
    }
}
