using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolbox.NFC.Reader.Card
{
    public class MifareUltralight
    {
        internal readonly SmartCard _smartCard;

        public MifareUltralight(SmartCard smartCard)
        {
            if (!smartCard.IsCardConnected())
                throw new Exception("No card attached");
            _smartCard = smartCard;
        }
    }
}
