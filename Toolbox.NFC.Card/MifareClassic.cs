using Toolbox.NFC.Reader.Apdu;
using Toolbox.NFC.Enums;
namespace Toolbox.NFC.Card;

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
    /// Load key into reader
    /// </summary>
    /// <param name="key">Key data</param>
    /// <returns>Success</returns>
    public virtual bool LoadKey(byte[] key)
    {
        ApduCommand apduCommand = new Commands.MifareClassicLoadKeyCommand(key, _smartCard.GetReaderType());
        return _smartCard.Execute(apduCommand).Success;
    }

    /// <summary>
    /// Authoruze mifare classic sector
    /// </summary>
    /// <param name="key">Key</param>
    /// <param name="keyType">Key type</param>
    /// <param name="sector">Data sector</param>
    /// <returns>Success</returns>
    public virtual bool Authorize(byte[] key, KeyType keyType, int sector)
    {
        if(!LoadKey(key)) return false;

        var command = new Commands.MifareClassicAuthorizeCommand(sector, keyType, _smartCard.GetReaderType());
        return _smartCard.Execute(command).Success;
    }

    /// <summary>
    /// Read block data
    /// </summary>
    /// <param name="sector">Chip sector</param>
    /// <param name="block">block in sector</param>
    /// <returns>byte[]</returns>
    public virtual byte[]? Read(int sector, int block)
    {
        var command = new Commands.MifareClassicReadCommand(sector, block, _smartCard.GetReaderType());

        var response = _smartCard.Execute(command);
        if (!response.Success) return null;
        return response.ResponseData;
    }


}
