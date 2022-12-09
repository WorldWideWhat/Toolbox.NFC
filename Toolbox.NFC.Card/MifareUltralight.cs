using Toolbox.NFC.Reader.Apdu;

namespace Toolbox.NFC.Card;

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
    public byte[]? ReadPage(int pageno)
    {
        var apduCommand = new Commands.MifareUltralightReadPageCommand(pageno, _smartCard.GetReaderType());
        var response = _smartCard.Execute(apduCommand);
        return response.Success ? response.ResponseData : null;
    }

    /// <summary>
    /// Write page data to Ultralight chip
    /// </summary>
    /// <param name="pageno">Page number</param>
    /// <param name="data">Page data</param>
    /// <returns>Success</returns>
    public bool WritePage(int pageno, byte[] data) 
    {
        var apduCommand = new Commands.MifareUltralightWritePageCommand(pageno, data, _smartCard.GetReaderType());
        return _smartCard.Execute(apduCommand).Success;
    }


}
