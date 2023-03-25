using System.Collections.Generic;
using Toolbox.NFC.Reader.Apdu;

namespace Toolbox.NFC.Reader.Driver.ACR
{
    internal sealed class MifareUltralightCAuthorizeCommand : ApduCommand
    {
        public MifareUltralightCAuthorizeCommand(byte[] authKey)
        {
            base.CLA = 0xFF;
            base.INS = 0x00;
            base.P1 = 0x00;
            base.P2 = 0x00;
            var payload = new List<byte>()
            {
                0xD4,
                0x42,
                0xAF
            };
            payload.AddRange(authKey);
            base.CommandData = payload.ToArray();
        }
    }
}
