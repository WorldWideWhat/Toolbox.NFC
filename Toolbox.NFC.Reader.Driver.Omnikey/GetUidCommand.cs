using Toolbox.NFC.Reader.Apdu;

namespace Toolbox.NFC.Reader.Driver.Omnikey
{
    public sealed class GetUidCommand : ApduCommand
    {
        public GetUidCommand()
            : base(0xff, 0xCA, 0x00, 0x00) { }
    }
}
