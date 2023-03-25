using Toolbox.NFC.Reader.Apdu;
using Toolbox.NFC.Enums;

namespace Toolbox.NFC.Reader.Driver.Omnikey
{
    /// <summary>
    /// Authorize Mifare classic sector on Omnikey reader
    /// </summary>
    internal class MifareClassicAuthorizeCommand : ApduCommand
    {
        public MifareClassicAuthorizeCommand(KeyType keyType, int sector)
            : base(0xFF, 0x86, 0x00, 0x00, new byte[] {0x01, 0x00, (byte)(sector * 4), (byte)keyType, 0x00})
        {

        }
    }
}
