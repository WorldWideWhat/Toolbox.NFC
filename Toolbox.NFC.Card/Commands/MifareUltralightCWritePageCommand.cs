using Toolbox.NFC.Reader.Driver;

namespace Toolbox.NFC.Card.Commands;
public class MifareUltralightCWritePageCommand : MifareUltralightWritePageCommand
{
    public MifareUltralightCWritePageCommand(int pageno, byte[] data, ReaderType reader) 
        : base(pageno, data, reader)
    {
    }
}
