using System;
using Toolbox.NFC.Reader.Apdu;
using Toolbox.NFC.Reader.Driver;

namespace Toolbox.NFC.Card.Commands
{
    internal class MifareUltralightReadPageCommand
    {
        public static ApduCommand Get(int page, ReaderType readerType)
        {
            if(readerType.Equals(ReaderType.Unsupported))
                throw new Exception("Unsupported reader");
            var apdu = new ApduCommand();
            switch (readerType)
            {
                case ReaderType.Omnikey:
                    apdu = new Reader.Driver.Omnikey.MifareUltralightReadPageCommand(page);
                    break;
                case ReaderType.ACR:
                    apdu = new Reader.Driver.ACR.MifareUltralightReadPageCommand(page);
                    break;
            }
            return apdu;
        }
    }
}