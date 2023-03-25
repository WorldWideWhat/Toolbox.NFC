namespace Toolbox.NFC.Tools
{
    /// <summary>
    /// Toolset for binary values
    /// </summary>
    public static class BinaryTools
    {
        /// <summary>
        /// Check if a flag/bit is set
        /// </summary>
        /// <param name="mask">Data mask</param>
        /// <param name="flag">Flag value</param>
        /// <returns>Boolean</returns>
        public static bool IsFlagSet(uint mask, uint flag)
            => (mask & flag) > 0;

        /// <summary>
        /// Compare if flag changed
        /// </summary>
        /// <param name="mask1">Data mask 1</param>
        /// <param name="mask2">Data mask 2</param>
        /// <param name="flag">Compare flag</param>
        /// <returns>Changed</returns>
        public static bool FlagChanged(uint mask1, uint mask2, uint flag)
            => (IsFlagSet(mask1, flag) != IsFlagSet(mask2, flag));
        

        /// <summary>
        /// Shift value number of bits left on a 32 bit value
        /// </summary>
        /// <param name="val">Value</param>
        /// <param name="bits">Number of bits to shift</param>
        /// <returns>Shifted value</returns>
        public static uint ShiftLeft(uint val, int bits)
            => (val << (bits > 32 ? 32 : bits)) & uint.MaxValue;

        /// <summary>
        /// Shift value number of bits left on a 16 bit value
        /// </summary>
        /// <param name="val">Value</param>
        /// <param name="bits">Number of bits to shift</param>
        /// <returns>Shifted value</returns>
        public static ushort ShiftLeft(ushort val, int bits)
            => (ushort)((val << (bits > 16 ? 16 : bits)) & ushort.MaxValue);

        /// <summary>
        /// Shift value number of bits left on a 8 bit value
        /// </summary>
        /// <param name="val">Value</param>
        /// <param name="bits">Number of bits to shift</param>
        /// <returns>Shifted value</returns>        
        public static byte ShiftLeft(byte val, int bits)
            => (byte)((val << (bits > 8 ? 8 : bits)) & byte.MaxValue);

        /// <summary>
        /// Shift value number of bits right on a 32 bit value
        /// </summary>
        /// <param name="val">Value</param>
        /// <param name="bits">Number of bits to shift</param>
        /// <returns>Shifted value</returns>        
        public static uint ShiftRight(uint val, int bits)
            => (val >> (bits > 32 ? 32 : bits)) & uint.MaxValue;

        /// <summary>
        /// Shift value number of bits right on a 16 bit value
        /// </summary>
        /// <param name="val">Value</param>
        /// <param name="bits">Number of bits to shift</param>
        /// <returns>Shifted value</returns>        
        public static ushort ShiftRight(ushort val, int bits)
            => (ushort)((val >> (bits > 16 ? 16 : bits)) & ushort.MaxValue);

        /// <summary>
        /// Shift value number of bits right on a 8 bit value
        /// </summary>
        /// <param name="val">Value</param>
        /// <param name="bits">Number of bits to shift</param>
        /// <returns>Shifted value</returns>        
        public static byte ShiftRight(byte val, int bits)
            => (byte)((val >> (bits > 8 ? 8 : bits)) & byte.MaxValue);

        /// <summary>
        /// Set single bit
        /// </summary>
        /// <param name="val">Value</param>
        /// <param name="bit">Bit number</param>
        /// <returns>uint</returns>
        public static uint SetBit(uint val, int bit) 
            => (uint)(val | (uint)(1 << bit > 32 ? 32 : bit));

        /// <summary>
        /// Unset single bit
        /// </summary>
        /// <param name="val">Value</param>
        /// <param name="bit">Bit number</param>
        /// <returns>uint</returns>
        public static uint UnsetBit(uint val, int bit)
            => (uint)(val & ~(1 << bit > 32 ? 32 : bit));
        
        /// <summary>
        /// Toggle a single bit
        /// </summary>
        /// <param name="val">Value</param>
        /// <param name="bit">Bit number</param>
        /// <returns>uint</returns>
        public static uint ToggleBit(uint val, int bit)
            => (uint)(val ^ (1 << bit > 32 ? 32 : bit));

        /// <summary>
        /// Check if a bit is set
        /// </summary>
        /// <param name="val">Value</param>
        /// <param name="bit">Bit number</param>
        /// <returns>boolean</returns>
        public static bool IsBitSet(uint val, int bit)
            => (val & (1 << bit > 32 ? 32 : bit)) > 0;
        
    }
}
