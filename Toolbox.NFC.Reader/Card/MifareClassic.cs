using Toolbox.NFC.Reader.Apdu;

namespace Toolbox.NFC.Reader.Card
{
    public class MifareClassic
    {
        private readonly SmartCard _smartCard;

        public MifareClassic(SmartCard smartCard)
        {
            if(!smartCard.IsCardConnected())
                throw new Exception("No card attached");
            _smartCard = smartCard;
        }

        /// <summary>
        /// Get card
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public virtual byte[]? GetUID()
        {
            ApduCommand apduCommand = new Commands.GetUidCommand(_smartCard.GetReaderType());

            var response = _smartCard.Execute(apduCommand);
            if (!response.Success)
                return null;
            return response.ResponseData;

        }

    }
}
