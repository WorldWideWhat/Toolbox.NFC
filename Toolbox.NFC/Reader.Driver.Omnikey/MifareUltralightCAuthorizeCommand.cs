using System.Collections.Generic;
using Toolbox.NFC.Reader.Apdu;

namespace Toolbox.NFC.Reader.Driver.Omnikey
{
    internal sealed class MifareUltralightCAuthorizeCommand : ApduCommand
    {
        public MifareUltralightCAuthorizeCommand(byte[] authKey)
            : base(0xFF, 0xA0, 0x00, 0x05)
        {
            List<byte> payload = new List<byte>()
            {
                0x01, 0x00, 0xF3, 0x00, 0x00, 0x64, 0xAF
            };
            payload.AddRange(authKey);
            base.CommandData = payload.ToArray();
        }

        public override byte[] GetBuffer()
        {
            var buffer = new List<byte>
            {
                CLA,
                INS,
                P1,
                P2,
                (byte)CommandData.Length
            };
            buffer.AddRange(CommandData);
            buffer.Add(0x00);
            return buffer.ToArray();
        }
    }
}
