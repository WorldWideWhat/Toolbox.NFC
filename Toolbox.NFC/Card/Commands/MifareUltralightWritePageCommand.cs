﻿using System;
using Toolbox.NFC.Reader.Apdu;
using Toolbox.NFC.Reader.Driver;

namespace Toolbox.NFC.Card.Commands
{
    internal class MifareUltralightWritePageCommand
    {
        public static ApduCommand Get(int pageno, byte[] data, ReaderType readerType)
        {
            if(readerType.Equals(ReaderType.Unsupported))
                throw new Exception("Unsupported reader");
            var apdu = new ApduCommand();
            switch (readerType)
            {
                case ReaderType.Omnikey:
                    apdu = new Reader.Driver.Omnikey.MifareUltralightWritePageCommand(pageno, data);
                    break;
                case ReaderType.ACR:
                    apdu = new Reader.Driver.ACR.MifareUltralightWritePageCommand(pageno, data);
                    break;
            }
            return apdu;
        }
    }
}