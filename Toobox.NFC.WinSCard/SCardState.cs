namespace Toobox.NFC.WinSCard
{
    public static class SCardState
    {
        public static readonly uint Unaware = 0x00000000;
        public static readonly uint Ignore = 0x00000001;
        public static readonly uint Changed = 0x00000002;
        public static readonly uint Unknown = 0x00000004;
        public static readonly uint Unavailable = 0x00000008;
        public static readonly uint Empty = 0x00000010;
        public static readonly uint Present = 0x00000020;
        public static readonly uint Atrmatch = 0x00000040;
        public static readonly uint Exclusive = 0x00000080;
        public static readonly uint Inuse = 0x00000100;
        public static readonly uint Mute = 0x00000200;
        public static readonly uint Unpowered = 0x00000400;
    }
}
