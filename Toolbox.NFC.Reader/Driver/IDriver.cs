using Toolbox.NFC.Reader.Apdu;

namespace Toolbox.NFC.Reader.Driver
{
    public interface IDriver
    {
        ApduResponse Execute(ApduCommand command);
        Task<ApduResponse> ExecuteAsync(ApduCommand command);
    }
}