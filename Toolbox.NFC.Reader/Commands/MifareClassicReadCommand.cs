using Toolbox.NFC.Reader.Apdu;
using Toolbox.NFC.Reader.Driver;

namespace Toolbox.NFC.Reader.Commands
{
    public sealed class MifareClassicReadCommand : ApduCommand
    {
        public MifareClassicReadCommand(int sector, int block, ReaderType readerType)
        {
            byte n_block = (byte)((sector * 4) + block);
            base.CLA = 0xff;
            base.INS = 0xB0;
            base.P1 = 0x00;
            base.P2 = n_block;
            base.CommandData = new byte[0x10];
            if (readerType == ReaderType.Unsupported)
                throw new Exception("Unsupported reader");
        }
    }
}
