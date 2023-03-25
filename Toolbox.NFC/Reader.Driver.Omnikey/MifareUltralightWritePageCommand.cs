using Toolbox.NFC.Reader.Apdu;

namespace Toolbox.NFC.Reader.Driver.Omnikey
{
	internal class MifareUltralightWritePageCommand : ApduCommand
	{
		public MifareUltralightWritePageCommand(int pageno, byte[] data)
			: base(0xFF, 0x0A, 0x00, 0x05)
		{
			base.CommandData = new byte[] { 0x01, 0x00, 0xF3, 0x00, 0x00, 0x64, 0xA2, (byte)pageno, 0, 0, 0, 0 };
			for (var index = 0; index < data.Length; index++)
			{
				if (index > 3) break;
				base.CommandData[index + 8] = data[index];
			}
		}
	}
}