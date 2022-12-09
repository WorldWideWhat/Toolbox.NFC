using Toolbox.NFC.Reader.Apdu;

namespace Toolbox.NFC.Reader.Driver.Omnikey
{
    public class MifareClassicLoadKeyCommand : ApduCommand
    {
        /// <summary>
        /// Load key into reader memory
        /// </summary>
        /// <param name="key">Load key</param>
        public MifareClassicLoadKeyCommand(byte[] key)
            :base(0xFF, 0x82, 0x20, 0x00, key)
        {
        }
    }
}
