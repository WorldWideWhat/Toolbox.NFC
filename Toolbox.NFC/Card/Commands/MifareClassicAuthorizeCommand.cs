using System;
using Toolbox.NFC.Enums;
using Toolbox.NFC.Reader.Apdu;
using Toolbox.NFC.Reader.Driver;

namespace Toolbox.NFC.Card.Commands
{
    internal sealed class MifareClassicAuthorizeCommand : ApduCommand
    {
        public static ApduCommand Get(int sector, KeyType keyType, ReaderType readerType)
        {
            if(readerType.Equals(ReaderType.Unsupported))
                throw new Exception("Unsupported reader");
            return readerType.Equals(ReaderType.Omnikey) ?
                new Reader.Driver.Omnikey.MifareClassicAuthorizeCommand(keyType, sector) :
                new ApduCommand(0xFF, 0x86, 0x00, 0x00, new byte[] { 0x01, 0x00, (byte)(sector * 4), (byte)keyType, 0x00 });
        }

        public MifareClassicAuthorizeCommand(int sector, KeyType keyType, ReaderType reader)
        {
            if (reader == ReaderType.Unsupported)
                throw new Exception("Unsupported reader");

            var apdu = reader == ReaderType.Omnikey ?
                new Reader.Driver.Omnikey.MifareClassicAuthorizeCommand(keyType, sector) :
                new ApduCommand(0xFF, 0x86, 0x00, 0x00, new byte[] { 0x01, 0x00, (byte)(sector * 4), (byte)keyType, 0x00 });

            base.CLA = apdu.CLA;
            base.INS = apdu.INS;
            base.P1 = apdu.P1;
            base.P2 = apdu.P2;
            base.CommandData= apdu.CommandData;
            base.Le= apdu.Le;
        }
    }
}
