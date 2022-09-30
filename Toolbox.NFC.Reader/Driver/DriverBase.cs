using Toobox.NFC.WinSCard;
using Toolbox.NFC.Reader.Apdu;
using WinSCard = Toobox.NFC.WinSCard.WinSCardInterop;

namespace Toolbox.NFC.Reader.Driver
{
    public class DriverBase : IDriver
    {

        public readonly IntPtr _hCard;

        public DriverBase(IntPtr hCard)
        {
            _hCard = hCard;
        }

        public virtual ApduResponse Execute(ApduCommand command)
        {
            SCard_IO_Request sioreq;
            sioreq.dwProtocol = 0x02;
            sioreq.cbPciLength = 8;

            SCard_IO_Request rioreq;
            rioreq.dwProtocol = 0x02;
            rioreq.cbPciLength = 8;
            var rxBufferLen = 255;
            var rxBuffer = new byte[rxBufferLen];

            var txBuffer = command.GetBuffer();

            var adpuResponse = new ApduResponse();

            var ret = WinSCard.SCardTransmit(_hCard, ref sioreq, ref txBuffer[0], txBuffer.Length + 1, ref rioreq, ref rxBuffer[0], ref rxBufferLen);
            if (ret != ErrorCode.SCARD_S_SUCCESS)
            {
                throw new Exception($"Failed to execute SCardTransmit. Error code {ret}");
            }
            adpuResponse.ExtractResponse(rxBuffer, rxBufferLen);
            return adpuResponse;
        }

        public virtual Task<ApduResponse> ExecuteAsync(ApduCommand command)
        {
            return Task.Run(() => Execute(command));
        }

    }
}
