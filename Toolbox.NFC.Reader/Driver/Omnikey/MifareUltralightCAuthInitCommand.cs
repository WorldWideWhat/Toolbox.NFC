using Toolbox.NFC.Reader.Apdu;

namespace Toolbox.NFC.Reader.Driver.Omnikey
{
    internal class MifareUltralightCAuthInitCommand : ApduCommand
    {
        public MifareUltralightCAuthInitCommand()
            : base(0xFF, 0xA0, 0x00, 0x05,new byte[]{ 0x01, 0x00, 0xF3, 0x00, 0x00, 0x64, 0x1A, 0x00})
        {}
    }
}
