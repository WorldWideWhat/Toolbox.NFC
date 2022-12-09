using Toolbox.NFC.Reader.Apdu;
using Toolbox.NFC.Reader.Driver;

namespace Toolbox.NFC.Card.Commands;
public sealed class MifareUltralightCAuthorizeCommand : ApduCommand
{
    public MifareUltralightCAuthorizeCommand(byte[] authKey, ReaderType readerType)
    {
        if (readerType == ReaderType.Unsupported)
            throw new Exception("Unsupported reader");

        var apdu = readerType switch
        {
            ReaderType.Omnikey => new Reader.Driver.Omnikey.MifareUltralightCAuthorizeCommand(authKey),
            ReaderType.ACR => new Reader.Driver.ACR.MifareUltralightCAuthorizeCommand(authKey),
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
