using System;
using Toolbox.NFC.Reader.Apdu;
using Toolbox.NFC.Reader.Driver;

namespace Toolbox.NFC.Card.Commands
{
    internal sealed class MifareUltralightCAuthInitCommand
    {
        public static ApduCommand Get(ReaderType readerType)
        {
            if (readerType == ReaderType.Unsupported)
                throw new Exception("Unsupported reader");
            var apdu = new ApduCommand();
            switch (readerType)
            {
                case ReaderType.Omnikey:
                    apdu = new Reader.Driver.Omnikey.MifareUltralightCAuthInitCommand();
                    break;
                case ReaderType.ACR:
                    apdu = new Reader.Driver.ACR.MifareUltralightCAuthInitCommand();
                    break;
            }
            return apdu;
        }
    }
}
