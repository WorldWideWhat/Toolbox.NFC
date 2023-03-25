using System;
using Toolbox.NFC.Reader.Apdu;
using Toolbox.NFC.Reader.Driver;

namespace Toolbox.NFC.Card.Commands
{
    internal sealed class MifareUltralightCAuthInitCommand : ApduCommand
    {
        public MifareUltralightCAuthInitCommand(ReaderType reader)
            :base(0xFF, 0xA0, 0x00, 0x05,new byte[] { 0x01, 0x00, 0xF3, 0x00, 0x00, 0x64, 0x1A, 0x00 })
        {
            
            if (reader == ReaderType.Unsupported)
                throw new Exception("Unsupported reader");

/*
            var apdu = new ApduCommand();
            switch(reader)
            {
                case ReaderType.Omnikey:
                    apdu = new Reader.Driver.Omnikey.MifareUltralightCAuthInitCommand();
                    break;
                case ReaderType.ACR:
                    apdu = new Reader.Driver.ACR.MifareUltralightCAuthInitCommand();
                    break;
            }

            base.CLA = apdu.CLA;
            base.INS = apdu.INS;
            base.P1 = apdu.P1;
            base.P2 = apdu.P2;
            base.CommandData = apdu.CommandData;
            base.Le = apdu.Le;
*/
        }
    }
}
