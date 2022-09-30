// See https://aka.ms/new-console-template for more information
using Toolbox.NFC.Reader;
using Toolbox.NFC.Reader.Card;

Console.WriteLine("Hello, World!");


var readers = Reader.Readers;

foreach (var reader in readers)
    Console.WriteLine($"Name: {reader}");

if(readers.Count > 0)
{
    using var reader = new Reader(readers[readers.Count -1]);
    if(reader.ConnectReader())
    {
        Console.WriteLine("Reader connected");
        using var smartCard = reader.GetSmartCard();
        if(smartCard.IsCardConnected())
        {
            Console.WriteLine("Card connected");
            if(smartCard.GetConnectedCardType() == Toolbox.NFC.Reader.Card.CardType.Mifare_Standard_1k)
            {
                var mifare = new MifareClassic(smartCard);
                var uid = mifare.GetUID();
                Console.WriteLine(BitConverter.ToString(uid));
            }
        }
    }

}

Console.ReadLine();