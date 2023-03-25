using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Toolbox.NFC.Reader;

namespace WinformsTest
{
    public partial class FormMain : Form
    {
        private Reader _reader = null;
        private volatile bool _cardPressent = false;

        public FormMain()
        {
            InitializeComponent();

        }

        /// <summary>
        /// Connect reader, and attach events
        /// </summary>
        /// <param name="readerName">Name of the reader</param>
        /// <returns>Success</returns>
        private bool ConnectReader(string readerName)
        {
            if (!_reader.ConnectReader()) return false;

            _reader.OnCardPresentChangedEvent += _reader_OnCardPresentChangedEvent;
            _reader.OnReaderDisconnectedEvent += _reader_OnReaderDisconnectedEvent;
            return true;
        }

        /// <summary>
        /// Reader disconnected 
        /// </summary>
        private void _reader_OnReaderDisconnectedEvent(object sender, EventArgs e)
        {
            _reader.OnCardPresentChangedEvent -= _reader_OnCardPresentChangedEvent;
            _reader.OnReaderDisconnectedEvent -= _reader_OnReaderDisconnectedEvent;
            _reader.Dispose();
        }

        /// <summary>
        /// Card pressens changed
        /// </summary>
        private void _reader_OnCardPresentChangedEvent(object sender, Toolbox.NFC.Reader.Event.CardPresentStateArgs e)
        {
            var reader = sender as Reader;
            
            var smartCard = reader?.GetSmartCard();
            
            _cardPressent = e.Pressent;

            if (e.Pressent)
            {
                if (smartCard is null) return;
                var uid = smartCard.GetUID();

                var cardType = smartCard.GetConnectedCardType();
                if (cardType != Toolbox.NFC.Enums.CardType.Mifare_Standard_1k)
                {
                    smartCard?.DisconnectCard();
                    MessageBox.Show("Unsupported card type");
                    return;
                }
            }
            else
            {
                smartCard?.DisconnectCard();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var readers = Reader.Readers;
            if (readers.Any())
            {
                var readerName = readers.Last();
                _reader = new Reader(readerName);
                if (!ConnectReader(readerName))
                {
                    MessageBox.Show($"Unable to connect to {readerName}");
                    Application.Exit();
                    return;
                }
            }
        }
    }
}
