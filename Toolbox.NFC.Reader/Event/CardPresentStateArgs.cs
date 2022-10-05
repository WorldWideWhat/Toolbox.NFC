﻿namespace Toolbox.NFC.Reader.Event
{
    public sealed class CardPresentStateArgs : EventArgs
    {
        public readonly string ReaderName;
        public readonly bool Pressent;

        public CardPresentStateArgs(string readerName, bool connected)
        {
            ReaderName = readerName;
            Pressent = connected;
        }
    }
}
