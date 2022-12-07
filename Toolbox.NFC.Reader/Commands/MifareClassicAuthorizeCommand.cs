using Toolbox.NFC.Reader.Apdu;
using Toolbox.NFC.Reader.Card;
using Toolbox.NFC.Reader.Driver;

namespace Toolbox.NFC.Reader.Commands
{
    public sealed class MifareClassicAuthorizeCommand : ApduCommand
    {
        private readonly ReaderType _readerType;
        public MifareClassicAuthorizeCommand(int sector, MifareClassic.KeyType keyType, ReaderType reader)
        {
            byte n_sector = (byte)(sector * 4);
            base.CLA = 0xFF;
            base.INS = 0x86;
            base.P1 = 0x00;
            base.P2 = 0x00;
            base.CommandData = new byte[5] { 0x01, 0x00, n_sector, (byte)keyType, 0x00 };
            _readerType = reader;
            if (_readerType == ReaderType.Unsupported)
                throw new Exception("Unsupported reader");

        }
    }
}
