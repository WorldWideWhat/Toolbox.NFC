namespace Toolbox.NFC.Reader.Driver
{
    internal class DriverHandler
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
            if (name.Contains("acr122") ||name.Contains("acr1252")) return ReaderType.ACR;
            return ReaderType.Unsupported;
        }
    }
}
