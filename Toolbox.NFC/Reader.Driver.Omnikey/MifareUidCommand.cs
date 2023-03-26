using Toolbox.NFC.Reader.Apdu;

namespace Toolbox.NFC.Reader.Driver.Omnikey
{
    internal sealed class MifareUidCommand : ApduCommand
    {
        public MifareUidCommand()
            : base(0xff, 0xCA, 0x00, 0x00) { }
    }
}
