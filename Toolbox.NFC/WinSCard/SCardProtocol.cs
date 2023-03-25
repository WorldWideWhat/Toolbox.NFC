namespace Toolbox.NFC.WinSCard
{
    /// <summary>
    /// Protocol types
    /// </summary>
    public sealed class SCardProtocol
    {
        public static readonly uint SCARD_PROTOCOL_T0 = 0x1;                  // T=0 is the active protocol.
        public static readonly uint SCARD_PROTOCOL_T1 = 0x2;                  // T=1 is the active protocol.
        public static readonly uint SCARD_PROTOCOL_UNDEFINED = 0x0;
        public static readonly uint SCARD_PROTOCOL_RAW = 0x10000;
        public static readonly uint SCARD_PROTOCOL_Tx = 0x3;
        public static readonly uint SCARD_PROTOCOL_DEFAULT = 0x80000000;
        public static readonly uint SCARD_PROTOCOL_OPTIMAL = 0;
    }
}
