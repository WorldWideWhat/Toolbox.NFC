using System;
using System.Collections.Generic;
using Toolbox.NFC.Reader.Apdu;
using Toolbox.NFC.Reader.Driver;

namespace Toolbox.NFC.Card.Commands
{
    internal sealed class MifareClassicWriteCommand : ApduCommand
    {
        public static ApduCommand Get(byte[] data, int sector, int block, ReaderType readerType)
        {
            if (readerType == ReaderType.Unsupported)
                throw new Exception("Unsupported reader");

            byte n_block = (byte)((sector * 4) + block);
            List<byte> cmdData = new List<byte>();
            for (var index = 0; index < data.Length; index++)
            {
                cmdData.Add(data[index]);
                if (index == 0x0F) break;
            }
            return new ApduCommand(0xFF, 0xD6, 0x00, n_block, cmdData.ToArray());
        }

        public MifareClassicWriteCommand(byte[] data, int sector, int block, ReaderType readerType)
        {
            byte n_block = (byte)((sector * 4) + block);
            base.CLA = 0xff;
            base.INS = 0xD6;
            base.P1 = 0x00;
            base.P2 = n_block;

            List<byte> cmdData = new List<byte>();
            for (var index = 0; index < data.Length; index++)
            {
                cmdData.Add(data[index]);
                if (index == 0x0F) break;
            }
            base.CommandData = cmdData.ToArray();

        }
    }
}
