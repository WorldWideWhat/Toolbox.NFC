using Toolbox.NFC.Enums;
using Toolbox.NFC.Reader.Driver;
using Toolbox.NFC.Reader.Driver.ACR;
using Toolbox.NFC.Reader.Driver.Omnikey;

namespace Toolbox.NFC.Card
{
    internal sealed class DriverHandler
    {
        /// <summary>
        /// Get supported reader type
        /// </summary>
        /// <param name="readerName">Name of the reader</param>
        /// <returns>SupportedReader</returns>
        public static ReaderType GetReaderType(string readerName)
        {
            var name = readerName.ToLower().Trim();

            if (name.Contains("omnikey")) return ReaderType.Omnikey;
            if (name.Contains("acr122") || name.Contains("acr1252")) return ReaderType.ACR;
            return ReaderType.Unsupported;
        }

        /// <summary>
        /// Get driver
        /// </summary>
        /// <param name="readerType">Selected reader type</param>
        /// <param name="hCard"></param>
        /// <returns>hCard pointer</returns>
        /// <exception cref="Exception">Unsupported reader will throw exception</exception>
        public static IDriver GetDriver(ReaderType readerType, IntPtr hCard)
        {
            return readerType switch
            {
                ReaderType.Omnikey => new OmnikeyDriver(hCard),
                ReaderType.ACR => new AcrDriver(hCard),
                _ => throw new Exception("Unsupported reader"),
            };
        }

        /// <summary>
        /// Get card type 
        /// </summary>
        /// <param name="readerType">Selected reader</param>
        /// <param name="cardTypeValue">Card type int value</param>
        /// <returns>CardType</returns>
        /// <exception cref="Exception">Unsupported reader will throw exception</exception>
        public static CardType GetCardType(ReaderType readerType, int cardTypeValue)
        {
            return readerType switch
            {
                ReaderType.Omnikey => OmnikeyDriver.GetCardType(cardTypeValue),
                ReaderType.ACR => AcrDriver.GetCardType(cardTypeValue),
                _ => throw new Exception("Unsupported reader"),
            };
        }
    }
}
