using System;
using Toolbox.NFC.Reader.Apdu;
using Toolbox.NFC.Reader.Driver;

namespace Toolbox.NFC.Card.Commands
{
    internal sealed class MifareUltralightCAuthorizeCommand : ApduCommand
    {
        public MifareUltralightCAuthorizeCommand(byte[] authKey, ReaderType readerType)
        {
            if (readerType == ReaderType.Unsupported)
                throw new Exception("Unsupported reader");

            var apdu = new ApduCommand();
            switch(readerType)
            {
                case ReaderType.Omnikey:
                    apdu = new Reader.Driver.Omnikey.MifareUltralightCAuthorizeCommand(authKey);
                    break;
                case ReaderType.ACR:
                    apdu = new Reader.Driver.ACR.MifareUltralightCAuthorizeCommand(authKey);
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