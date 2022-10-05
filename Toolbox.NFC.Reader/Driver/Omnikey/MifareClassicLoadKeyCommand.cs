using Toolbox.NFC.Reader.Apdu;

namespace Toolbox.NFC.Reader.Driver.Omnikey
{
    internal class MifareClassicLoadKeyCommand : ApduCommand
    {
        public MifareClassicLoadKeyCommand(byte[] key)
            :base(0xFF, 0x82, 0x20, 0x00, key)
        {

        }
    }
}
