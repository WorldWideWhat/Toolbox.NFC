using Toolbox.NFC.Reader.Driver;

namespace Toolbox.NFC.Card.Commands
{ 
    internal class MifareUltralightCReadPageCommand : MifareUltralightReadPageCommand
    {
        public MifareUltralightCReadPageCommand(int page, ReaderType reader) 
            : base(page, reader)
        {
        }
    }
}
