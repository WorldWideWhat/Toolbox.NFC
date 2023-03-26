using System;

namespace Toolbox.NFC.Card
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

        /// <summary>
        /// Read page data from Ultralight chip
        /// </summary>
        /// <param name="pageno">Page number</param>
        /// <returns>byte[]</returns>
        public byte[] ReadPage(int pageno)
        {
            var apduCommand = Commands.MifareUltralightReadPageCommand.Get(pageno, _smartCard.GetReaderType());
            var response = _smartCard.Execute(apduCommand);
            if (!response.Success) return null;
            var result = new byte[4];
            Array.Copy(response.ResponseData, 0, result, 0, 4);
            return result;
        }

        /// <summary>
        /// Write page data to Ultralight chip
        /// </summary>
        /// <param name="pageno">Page number</param>
        /// <param name="data">Page data</param>
        /// <returns>Success</returns>
        public bool WritePage(int pageno, byte[] data)
        {
            var apduCommand = Commands.MifareUltralightWritePageCommand.Get(pageno, data, _smartCard.GetReaderType());
            var response = _smartCard.Execute(apduCommand);
            return response.Success;
        }


    }
}