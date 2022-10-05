// See https://aka.ms/new-console-template for more information
using System.Reflection.PortableExecutable;
using Toolbox.NFC.Reader;
using Toolbox.NFC.Reader.Card;

Console.WriteLine("Hello, World!");

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
        
        //using var smartCard = reader.GetSmartCard();
        while (true)
        {
/*
            if (smartCard.IsCardConnected())
            {
                Console.WriteLine("Card connected");
                if (smartCard.GetConnectedCardType() == Toolbox.NFC.Reader.Card.CardType.Mifare_Standard_1k)
                {
                    var mifare = new MifareClassic(smartCard);
                    var uid = mifare.GetUID();
                    Console.WriteLine(BitConverter.ToString(uid));
                    Console.ReadLine();
                    break;
                }
            }
*/
            await Task.Delay(100);
        }
    }

}

void Reader_OnCardPresentChangedEvent(object? sender, Toolbox.NFC.Reader.Event.CardPresentStateArgs e)
{
    if (sender is null) return;

    
    var reader = sender as Reader;
    var smartCard = reader.GetSmartCard();

    if(e.Pressent)
    {
        if (smartCard is null)
        {
            Console.WriteLine("NULL");
            return;
        }
        var cardType = smartCard.GetConnectedCardType();
        Console.WriteLine("Present");

        var uid = smartCard.GetUID();
        if(uid != null)
            Console.WriteLine($"UID: {BitConverter.ToString(uid)}");

    }
    else
    {
        smartCard.DisconnectCard();
    }

    Console.WriteLine($"Card present: {e.Pressent} on reader {e.ReaderName}");
}

Console.ReadLine();