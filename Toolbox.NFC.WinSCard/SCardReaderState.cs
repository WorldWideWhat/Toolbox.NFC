using System.Runtime.InteropServices;

namespace Toolbox.NFC.WinSCard
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct SCardReaderState
    {
        public string cardReaderString;
        public IntPtr userDataPointer;
        public uint currentState;
        public uint eventState;
        public uint atrLength;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 36)]
        public byte[] ATR;
    }
}
