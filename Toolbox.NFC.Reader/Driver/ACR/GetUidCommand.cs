using Toolbox.NFC.Reader.Apdu;

namespace Toolbox.NFC.Reader.Driver.ACR
{
    internal sealed class GetUidCommand : ApduCommand
    {
        public GetUidCommand()
            : base(0xff, 0xCA, 0x00, 0x00, new byte[]{0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00}) { }
    }
}
