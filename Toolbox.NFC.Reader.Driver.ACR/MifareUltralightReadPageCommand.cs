using Toolbox.NFC.Reader.Apdu;

namespace Toolbox.NFC.Reader.Driver.ACR;
public class MifareUltralightReadPageCommand : ApduCommand
{
    public MifareUltralightReadPageCommand(int page)
        :base(0xFF, 0xB0, 0x00, (byte)page, new byte[] {0x00, 0x00, 0x00, 0x00})
    {
    }
}
