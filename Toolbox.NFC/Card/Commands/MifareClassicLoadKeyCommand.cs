using System;
using Toolbox.NFC.Reader.Apdu;
using Toolbox.NFC.Reader.Driver;

namespace Toolbox.NFC.Card.Commands
{
    internal class MifareClassicLoadKeyCommand : ApduCommand
    {
        public static ApduCommand Get(byte[] key, ReaderType readerType)
        {
            if(readerType.Equals(ReaderType.Unsupported))
                throw new Exception("Unsupported reader");
            var apdu = new ApduCommand();
            switch (readerType)
            {
                case ReaderType.Omnikey:
                    apdu = new Reader.Driver.Omnikey.MifareClassicLoadKeyCommand(key);
                    break;
                case ReaderType.ACR:
                    apdu = new Reader.Driver.ACR.MifareClassicLoadKeyCommand(key);
                    break;
            }
            return apdu;
        }

        public MifareClassicLoadKeyCommand(byte[] key, ReaderType reader)
        {
            if (reader == ReaderType.Unsupported)
                throw new Exception("Unsupported reader");
            var apdu = new ApduCommand();
            switch(reader)
            {
                case ReaderType.Omnikey:
                    apdu = new Reader.Driver.Omnikey.MifareClassicLoadKeyCommand(key);
                    break;
                case ReaderType.ACR:
                    apdu = new Reader.Driver.ACR.MifareClassicLoadKeyCommand(key);
                    break;
            }

            base.CLA = apdu.CLA;
            base.INS = apdu.INS;
            base.P1 = apdu.P1;
            base.P2 = apdu.P2;
            base.CommandData = apdu.CommandData;
            base.Le = apdu.Le;
        }
    }
}