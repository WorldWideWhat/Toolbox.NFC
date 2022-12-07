using System.Runtime.InteropServices;

namespace Toolbox.NFC.WinSCard
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct SCard_ReaderState
    {
        public string RdrName;
        public string UserData;
        public uint RdrCurrState;
        public uint RdrEventState;
        public uint ATRLength;
        /// <summary>
        /// Fixed size return array
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x24, ArraySubType = UnmanagedType.U1)]
        public byte[] ATRValue;
    }
}
