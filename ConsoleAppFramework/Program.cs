using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Toolbox.NFC.Card;
using Toolbox.NFC.Reader;

namespace ConsoleAppFramework
{
    internal class Program
    {
        bool readerConnected = false;
        byte[] DATA_TO_WRITE = {
              0x39, 0x38, 0x37, 0x36, 0x35, 0x34, 0x33, 0x32, 0x31, 0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36
        };
        static void Main(string[] args)
        {
            var program = new Program();
            program.Run();
        }

        public Program() 
        {

        }

        public void Run()
        {
            var readers = Reader.Readers;
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
                using (var reader = new Reader(readerName))
                {
                    if (reader.ConnectReader())
                    {
                        // Attache callback event function
                        reader.OnCardPresentChangedEvent += (sender, e) =>
                        {
                            if (sender is null) return;


                            var n_reader = sender as Reader;
                            var smartCard = n_reader?.GetSmartCard();

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


                                byte[] uid = smartCard.GetUID();

                                var key = new byte[] { 0xff, 0xff, 0xff, 0xff, 0xff, 0xff };
                                var mifareCard = new MifareClassic(smartCard);
                                if (!mifareCard.Authorize(key, Toolbox.NFC.Enums.KeyType.KeyA, 2))
                                {
                                    Console.WriteLine("Unable to authorize card");
                                    return;
                                }
                                if (!mifareCard.Write(DATA_TO_WRITE, 2, 1))
                                {
                                    Console.WriteLine("Unable to write data to sector");
                                    return;
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
                        /*
                                reader.OnCardInUseChangedEvent += (sender, e) =>
                                {
                                    Console.WriteLine($"In use: {e.InUse}");
                                };
                        */
                        Console.WriteLine("Reader connected");
                        readerConnected = true;
                        while (readerConnected)
                        {
                            System.Threading.Thread.Sleep(100);
                        }
                        Console.WriteLine("Application Ended");
                    }
                }
            }
        }
    }

    
}
