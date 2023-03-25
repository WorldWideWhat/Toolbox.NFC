using System.Threading.Tasks;
using Toolbox.NFC.Reader.Apdu;

namespace Toolbox.NFC.Reader.Driver
{
    internal interface IDriver
    {
        ApduResponse Execute(ApduCommand command, int txLen = -1);
        
        ApduResponse Execute(byte[] data, int len = -1);

        Task<ApduResponse> ExecuteAsync(ApduCommand command, int txLen = -1);
    }
}