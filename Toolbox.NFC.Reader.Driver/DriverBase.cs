using Toolbox.NFC.WinSCard;
using Toolbox.NFC.Reader.Apdu;
using SCard = Toolbox.NFC.WinSCard.WinSCardInterop;

namespace Toolbox.NFC.Reader.Driver
{
    /// <summary>
    /// Base driver
    /// </summary>
    public class DriverBase : IDriver
    {
        /// <summary>
        /// Card object
        /// </summary>
        public readonly IntPtr _hCard;

        public DriverBase(IntPtr hCard)
        {
            _hCard = hCard;
        }

        /// <summary>
        /// Execute APDU command
        /// </summary>
        /// <param name="command">Command</param>
        /// <returns>ApduResponse</returns>
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
#if DEBUG
            Console.WriteLine($"Command data: {BitConverter.ToString(txBuffer)}");
#endif
            var adpuResponse = new ApduResponse();

            var ret = SCard.SCardTransmit(_hCard, ref sioreq, ref txBuffer[0], txBuffer.Length + 1, ref rioreq, ref rxBuffer[0], ref rxBufferLen);
            if (ret != ErrorCode.SCARD_S_SUCCESS)
            {
                return adpuResponse;
            }
            adpuResponse.ExtractResponse(rxBuffer, rxBufferLen);
            return adpuResponse;
        }

        /// <summary>
        /// Execute APDU Command Async
        /// </summary>
        /// <param name="command">Command</param>
        /// <returns>ApduResponse</returns>
        public virtual Task<ApduResponse> ExecuteAsync(ApduCommand command) => Task.Run(() => Execute(command));

    }
}
