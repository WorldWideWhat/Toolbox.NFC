using Toolbox.NFC.Reader.Apdu;

namespace Toolbox.NFC.Reader.Driver.ACR
{
    internal class MifareUltralightCAuthInitCommand : ApduCommand
    {
        public MifareUltralightCAuthInitCommand()
            : base(0xFF, 0x00, 0x00, 0x00, new byte[] { 0xD4, 0x42, 0x1A, 0x00 }) { }
    }
}
