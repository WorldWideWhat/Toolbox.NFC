using System;
using Toolbox.NFC.Reader.Apdu;
using Toolbox.NFC.Reader.Driver;

namespace Toolbox.NFC.Card.Commands
{
    internal sealed class MifareClassicReadCommand : ApduCommand
    {
        public static ApduCommand Get(int sector, int block, ReaderType readerType)
        {
            if (readerType == ReaderType.Unsupported)
                throw new Exception("Unsupported reader");
            return new ApduCommand(0xFF, 0xB0, 0x00, (byte)((sector * 4) + block), new byte[0x10]);
        }

        public MifareClassicReadCommand(int sector, int block, ReaderType readerType)
            :base(0xFF, 0xB0, 0x00, (byte)((sector * 4) + block), new byte[0x10])
        {
            if (readerType == ReaderType.Unsupported)
                throw new Exception("Unsupported reader");
        }
    }
}
