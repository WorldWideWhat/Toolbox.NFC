using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.NFC.Reader.Apdu;
using static Toolbox.NFC.Reader.Card.MifareClassic;

namespace Toolbox.NFC.Reader.Driver.Omnikey
{
    internal class MifareClassicAuthorizeCommand : ApduCommand
    {
        public MifareClassicAuthorizeCommand(byte[] key, KeyType keyType, int sector)
        {

        }
    }
}
