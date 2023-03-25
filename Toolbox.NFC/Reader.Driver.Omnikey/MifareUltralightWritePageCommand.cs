using System.Collections.Generic;
using Toolbox.NFC.Reader.Apdu;

namespace Toolbox.NFC.Reader.Driver.Omnikey
{
	internal class MifareUltralightWritePageCommand : ApduCommand
	{
        public MifareUltralightWritePageCommand(int pageno, byte[]data)
            : base(0xFF, 0xD6, 0x00, (byte)pageno, data)
        {
            
        }
    }
}