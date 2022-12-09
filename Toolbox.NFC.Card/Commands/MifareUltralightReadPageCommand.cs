﻿using Toolbox.NFC.Reader.Apdu;
using Toolbox.NFC.Reader.Driver;

namespace Toolbox.NFC.Card.Commands;
public class MifareUltralightReadPageCommand : ApduCommand
{
    public MifareUltralightReadPageCommand(int page, ReaderType reader)
    {
        if (reader == ReaderType.Unsupported)
            throw new Exception("Unsupported reader");

        var apdu = reader switch
        {
            ReaderType.Omnikey => new Reader.Driver.Omnikey.MifareUltralightReadPageCommand(page),
            ReaderType.ACR => new Reader.Driver.ACR.MifareUltralightReadPageCommand(page),
            _ => new ApduCommand(),
        };

        base.CLA = apdu.CLA;
        base.INS = apdu.INS;
        base.P1 = apdu.P1;
        base.P2 = apdu.P2;
        base.CommandData = apdu.CommandData;
        base.Le= apdu.Le;
    }
}
