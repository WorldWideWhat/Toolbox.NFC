using Toolbox.NFC.Reader.Apdu;

namespace Toolbox.NFC.Reader.Driver.ACR
{
    internal class MifareUltralightWritePageCommand : ApduCommand
    {
        public MifareUltralightWritePageCommand(int pageno, byte[] data)
            : base(0xFF, 0x00, 0x00, 0x00)
        {
            base.CommandData = new byte[] { 0xd4, 0x40, 0x01, 0xa2, (byte)pageno, 0x00, 0x00, 0x00, 0x00 };
            for (var index = 0; index < data.Length; index++)
            {
                if (index > 3) break;
                base.CommandData[index + 5] = data[index];
            }
        }
    }
}