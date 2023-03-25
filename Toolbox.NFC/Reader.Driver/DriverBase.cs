using Toolbox.NFC.WinSCard;
using Toolbox.NFC.Reader.Apdu;
using SCard = Toolbox.NFC.WinSCard.WinSCardInterop;
using System;
using System.Threading.Tasks;

namespace Toolbox.NFC.Reader.Driver
{
    /// <summary>
    /// Base driver
    /// </summary>
    internal class DriverBase : IDriver
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
        public virtual ApduResponse Execute(ApduCommand command, int txLen = -1)
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
            var sendLen = txLen > -1 ? txLen : txBuffer.Length;
#if DEBUG
            Console.WriteLine($"Command data: {BitConverter.ToString(txBuffer)}");
            Console.WriteLine($"Command length: {sendLen}");
#endif
            var adpuResponse = new ApduResponse();

            var ret = SCard.SCardTransmit(_hCard, ref sioreq, ref txBuffer[0], sendLen, ref rioreq, ref rxBuffer[0], ref rxBufferLen);
#if DEBUG
            Console.WriteLine($"RespLen: {rxBufferLen}, Response: {BitConverter.ToString(rxBuffer)}");
#endif
            
            if (ret != ErrorCode.SCARD_S_SUCCESS)
            {
                return adpuResponse;
            }
            adpuResponse.ExtractResponse(rxBuffer, rxBufferLen);
            return adpuResponse;
        }

        public ApduResponse Execute(byte[] data, int len = -1)
        {
            SCard_IO_Request sioreq;
            sioreq.dwProtocol = 0x02;
            sioreq.cbPciLength = 8;

            SCard_IO_Request rioreq;
            rioreq.dwProtocol = 0x02;
            rioreq.cbPciLength = 8;
            var rxBufferLen = 255;
            byte[] rxBuffer = new byte[rxBufferLen];
            int sendLen = len > -1 ? len : data.Length + 1;
            var adpuResponse = new ApduResponse();

#if DEBUG
            Console.WriteLine($"Command data: {BitConverter.ToString(data)}");
            Console.WriteLine($"Command length: {sendLen}");
#endif

            var ret = SCard.SCardTransmit(_hCard, ref sioreq, ref data[0], sendLen, ref rioreq, ref rxBuffer[0], ref rxBufferLen);

            if (ret != ErrorCode.SCARD_S_SUCCESS)
            {
                return adpuResponse;
            }
            adpuResponse.ExtractResponse(rxBuffer, rxBufferLen);
#if DEBUG
            Console.WriteLine($"RespLen: {rxBufferLen}, Response: {BitConverter.ToString(rxBuffer)}");
#endif
            return adpuResponse;
        }

        /// <summary>
        /// Execute APDU Command Async
        /// </summary>
        /// <param name="command">Command</param>
        /// <returns>ApduResponse</returns>
        public virtual Task<ApduResponse> ExecuteAsync(ApduCommand command, int txLen = -1) => Task.Run(() => Execute(command, txLen));

    }
}
