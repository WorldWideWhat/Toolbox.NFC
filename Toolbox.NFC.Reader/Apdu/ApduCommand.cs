namespace Toolbox.NFC.Reader.Apdu
{
    public class ApduCommand
    {
        /// <summary>
        /// Class of instructions
        /// </summary>
        public byte CLA { get; set; }
        /// <summary>
        /// Instruction code
        /// </summary>
        public byte INS { get; set; }
        /// <summary>
        /// Instruction parameter 1
        /// </summary>
        public byte P1 { get; set; }
        /// <summary>
        /// Instruction parameter 2
        /// </summary>
        public byte P2 { get; set; }
        /// <summary>
        /// Maximum number of bytes expected in the response ot this command
        /// </summary>
        public byte? Le { get; set; }
        /// <summary>
        /// Contiguous array of bytes representing commands data
        /// </summary>
        public byte[]? CommandData { get; set; }
        /// <summary>
        /// Expected response type for this command.
        /// Provides mechanism to bind commands to responses
        /// </summary>
        public Type? ApduResponseType { set; get; }

        public ApduCommand() { }

        public ApduCommand(byte cla, byte ins, byte p1, byte p2, byte[]? commandData = null, byte? le = null)
        {
            if (commandData != null && commandData.Length > 254)
            {
                throw new NotImplementedException();
            }
            CLA = cla;
            INS = ins;
            P1 = p1;
            P2 = p2;
            CommandData = commandData;
            Le = le;

            ApduResponseType = typeof(ApduResponse);
        }

        /// <summary>
        /// Get Apdu buffer
        /// </summary>
        /// <returns></returns>
        public virtual byte[] GetBuffer()
        {
            var buffer = new List<byte>
            {
                CLA,
                INS,
                P1,
                P2
            };

            if (CommandData != null && CommandData.Length > 0)
            {
                buffer.Add((byte)CommandData.Length);
                buffer.AddRange(CommandData);
            }

            if (Le != null)
            {
                buffer.Add((byte)Le);
            }
            return buffer.ToArray();
        }
    }
}
