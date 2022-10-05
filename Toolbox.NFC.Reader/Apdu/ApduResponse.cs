namespace Toolbox.NFC.Reader.Apdu
{
    public class ApduResponse
    {
        public byte SW1 { get; set; } = 0x00;
        public byte SW2 { get; set; } = 0x00;

        public ushort SW
        {
            get { return (ushort)(SW1 << 8 | SW2); }
            set
            {
                SW1 = (byte)(value >> 8);
                SW2 = (byte)(value & 0xFF);
            }
        }

        public byte[]? ResponseData { get; set; }

        public bool Success => (SW == 0x9000);
    
    
        public virtual void ExtractResponse(byte[]? buffer, int bufferLen)
        {
            if(buffer is null)
                throw new ArgumentNullException(nameof(buffer));
            if (bufferLen < 2)
                throw new InvalidDataException("APDU response must be at least 2 bytes");

            ResponseData = new byte[bufferLen - 2];
            if (buffer.Length > 2)
            {
                Array.Copy(buffer, 0, ResponseData, 0, bufferLen - 2);    
            }
            SW1 = buffer[bufferLen-2];
            SW2 = buffer[bufferLen-1];
        }
    }
}
