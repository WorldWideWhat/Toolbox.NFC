using Toolbox.NFC.Reader.Apdu;

namespace Toolbox.NFC.Reader.Driver.ACR
{
    internal class MifareClassicLoadKeyCommand : ApduCommand
    {
        public MifareClassicLoadKeyCommand(byte[] key)
            : base(0xFF, 0x82, 0x00, 0x00, key)
        { }
    }
}
