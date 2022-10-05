namespace Toolbox.NFC.Reader.Event
{
    public sealed class CardInUseStateArgs : EventArgs
    {
        public readonly string ReaderName;
        public readonly bool InUse;

        public CardInUseStateArgs(string readerName, bool inUse)
        {
            ReaderName = readerName;
            InUse = inUse;
        }
    }
}
