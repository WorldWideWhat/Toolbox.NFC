using Toolbox.NFC.Reader.Driver;

namespace Toolbox.NFC.Card.Commands;

public class MifareUltralightCReadPageCommand : MifareUltralightReadPageCommand
{
    public MifareUltralightCReadPageCommand(int page, ReaderType reader) 
        : base(page, reader)
    {
    }
}
