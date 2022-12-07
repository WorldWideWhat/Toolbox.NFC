// See https://aka.ms/new-console-template for more information
using Toolbox.NFC.Reader;
var readers = Reader.Readers;

if(readers.Count > 0)
{
    var readerName = string.Empty;
    while (true)
    {
        Console.Clear();
        Console.WriteLine("Please select reader");
        for (int i = 0; i < readers.Count; i++)
        {
            Console.WriteLine($"{i}) {readers[i]}");
        }

        var key = Console.ReadKey();
        if (key.KeyChar >= '0' && key.KeyChar <= '9')
        {
            int rIndex = key.KeyChar - 0x30;
            if(rIndex < readers.Count)
            {
                readerName = readers[rIndex];
                break;
            }
        }
    }

    if (readerName == string.Empty) return;
    using var reader = new Reader(readerName);
    if(reader.ConnectReader())
    {
        reader.OnCardPresentChangedEvent += Reader_OnCardPresentChangedEvent;
        Console.WriteLine("Reader connected");
        
        while (true)
        {
            await Task.Delay(100);
        }
    }

}

void Reader_OnCardPresentChangedEvent(object? sender, Toolbox.NFC.Reader.Event.CardPresentStateArgs e)
{
    if (sender is null) return;


    var reader = sender as Reader;
    var smartCard = reader?.GetSmartCard();

    if (e.Pressent)
    {
        if (smartCard is null)
        {
            Console.WriteLine("NULL");
            return;
        }
        var cardType = smartCard.GetConnectedCardType();
        Console.WriteLine("Present");

        byte[]? uid = smartCard.GetUID();
        if (uid != null)
            Console.WriteLine($"UID: {BitConverter.ToString(uid)}");

        Console.WriteLine(cardType);

    }
    else
    {
        smartCard?.DisconnectCard();
    }

    Console.WriteLine($"Card present: {e.Pressent} on reader {e.ReaderName}");
}

Console.ReadLine();