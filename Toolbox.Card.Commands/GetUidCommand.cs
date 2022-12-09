using Toolbox.NFC.Reader.Apdu;
using Toolbox.NFC.Reader.Driver;

namespace Toolbox.NFC.Reader.Commands
{
    public class GetUidCommand : ApduCommand
    {
        private readonly ReaderType _type;
        public GetUidCommand(ReaderType reader)
        {
            _type = reader;
            if (_type == ReaderType.Unsupported)
                throw new Exception("Unsupported reader");
        }

        public override byte[] GetBuffer()
        {
            return _type switch
            {
                ReaderType.Omnikey => new Driver.Omnikey.GetUidCommand().GetBuffer(),
                ReaderType.ACR => new Driver.Omnikey.GetUidCommand().GetBuffer(),
                _ => throw new Exception("Unsupported reader"),
            };
        }
    }
}
