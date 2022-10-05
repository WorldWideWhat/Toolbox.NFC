using Toolbox.NFC.Reader.Apdu;

namespace Toolbox.NFC.Reader.Card
{
    public class MifareClassic
    {
        private readonly SmartCard _smartCard;

        public enum KeyType : byte
        {
            KeyA = 0x60,
            KeyB = 0x61,
        }

        public MifareClassic(SmartCard smartCard)
        {
            if(!smartCard.IsCardConnected())
                throw new Exception("No card attached");
            _smartCard = smartCard;
        }

        /// <summary>
        /// Load key into reader
        /// </summary>
        /// <param name="key">Key data</param>
        /// <returns>Success</returns>
        public virtual bool LoadKey(byte[] key)
        {
            ApduCommand apduCommand = new Commands.MifareClassicLoadKeyCommand(key, _smartCard.GetReaderType());
            return _smartCard.Execute(apduCommand).Success;
        }

        public virtual bool Authorize(byte[] key, KeyType keyType, int sector)
        {
            return false;
        }

    }
}
