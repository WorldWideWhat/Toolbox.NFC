using System.Globalization;
using Toolbox.NFC.Card;
using Toolbox.NFC.Reader;

// Create list of attached readers
var readers = Reader.Readers;
var readerConnected = false;
byte[] DATA_TO_WRITE = {
    0x39, 0x38, 0x37, 0x36, 0x35, 0x34, 0x33, 0x32, 0x31, 0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36
};

if (readers.Count > 0)
{
    var readerName = string.Empty;
    // Write selection menu to console and wait for key press
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
            if (rIndex < readers.Count)
            {
                readerName = readers[rIndex];
                break;
            }
        }
    }

    if (readerName == string.Empty) return;
    // Create reader object from selected reader
    using var reader = new Reader(readerName);
    if (reader.ConnectReader())
    {
        // Attache callback event function
        reader.OnCardPresentChangedEvent += (sender, e) =>
        {
            if (sender is null) return;


            var reader = sender as Reader;
            var smartCard = reader?.GetSmartCard();

            if (e.Pressent)
            {
                if (smartCard is null)
                {
#if DEBUG
                    Console.WriteLine("NULL");
#endif
                    return;
                }
                var cardType = smartCard.GetConnectedCardType();
                Console.WriteLine("Present");

                
                byte[]? uid = smartCard.GetUID();
                switch(smartCard.GetConnectedCardType())
                {
                    case Toolbox.NFC.Enums.CardType.Mifare_Standard_1k:
                    case Toolbox.NFC.Enums.CardType.Mifare_Standard_4k:
                        
                        var mifareCard = new MifareClassic(smartCard);
                        if (!mifareCard.Authorize(MifareClassic.DefaultKey, Toolbox.NFC.Enums.KeyType.KeyA, 2))
                        {
                            Console.WriteLine("Unable to authorize card");
                            return;
                        }
                        Console.WriteLine("Card authorized");
                        if (!mifareCard.Write(DATA_TO_WRITE, 2, 1))
                        {
                            Console.WriteLine("Unable to write data to sector");
                            return;
                        }
                        Console.WriteLine("Data written to sector 2 block 1");
                        break
                        ;
                    case Toolbox.NFC.Enums.CardType.Mifare_UltraLight_C:
                        var mifareULC = new MifareUltralightC(smartCard);

                        if(mifareULC.Authorize(MifareUltralightC.DefaultKey))
                        {
                            var tmpPage = new byte[] { 0x30, 0x31, 0x32, 0x33 };
                            var resp = mifareULC.WritePage(5, tmpPage);
                            var pages = new List<byte[]>();
                            for (var pageNo = 0; pageNo < 36; pageNo++)
                            {
                                var page = mifareULC.ReadPage(pageNo);
                                pages.Add(page);
                            }
                            
                            for(int pageNo = 0; pageNo < pages.Count; pageNo++)
                            {
                                Console.WriteLine($"{pageNo.ToString().PadLeft(2, '0')}: {BitConverter.ToString(pages[pageNo])}");
                            }
                        }

                        break;
                }

                if (uid != null)
                    Console.WriteLine($"UID: {BitConverter.ToString(uid)}");

                Console.WriteLine(cardType);

            }
            else
            {
                smartCard?.DisconnectCard();
            }

            Console.WriteLine($"Card present: {e.Pressent} on reader {e.ReaderName}");
        };
        reader.OnReaderDisconnectedEvent += (sender, e) =>
        {
            Console.WriteLine($"Reader {readerName} disconnected");
            readerConnected = false;
        };

        Console.WriteLine("Reader connected");
        readerConnected = true;
        while (readerConnected)
        {
            await Task.Delay(100);
        }
        Console.WriteLine("Application Ended");
    }

}
