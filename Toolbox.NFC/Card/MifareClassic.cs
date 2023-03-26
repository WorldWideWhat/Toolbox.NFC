using System;
using Toolbox.NFC.Enums;

namespace Toolbox.NFC.Card
{

    public class MifareClassic
    {
        public static readonly byte[] DefaultKey = new byte[] { 0xff, 0xff, 0xff, 0xff, 0xff, 0xff };
        private readonly SmartCard _smartCard;
        public MifareClassic(SmartCard smartCard)
        {
            if (!smartCard.IsCardConnected())
                throw new Exception("No card attached");
            _smartCard = smartCard;
        }

        /// <summary>
        /// Load key into reader
        /// </summary>
        /// <param name="key">Key data</param>
        /// <returns>Success</returns>
        public bool LoadKey(byte[] key)
        {
            var apduCommand = Commands.MifareClassicLoadKeyCommand.Get(key, _smartCard.GetReaderType());
            return _smartCard.Execute(apduCommand).Success;
        }

        /// <summary>
        /// Authoruze mifare classic sector
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="keyType">Key type</param>
        /// <param name="sector">Data sector</param>
        /// <returns>Success</returns>
        public bool Authorize(byte[] key, KeyType keyType, int sector)
        {
            if (!LoadKey(key)) return false;
            var apduCommand = Commands.MifareClassicAuthorizeCommand.Get(sector, keyType, _smartCard.GetReaderType());
            var result = _smartCard.Execute(apduCommand);
            return result.Success;
        }

        /// <summary>
        /// Read block data
        /// </summary>
        /// <param name="sector">Chip sector</param>
        /// <param name="block">block in sector</param>
        /// <returns>byte[]</returns>
        public byte[] Read(int sector, int block)
        {
            var apduCommand = Commands.MifareClassicReadCommand.Get(sector, block, _smartCard.GetReaderType());
            var response = _smartCard.Execute(apduCommand);
            if (!response.Success) return null;
            return response.ResponseData;
        }

        /// <summary>
        /// Write block data
        /// </summary>
        /// <param name="data">Sector data</param>
        /// <param name="sector">Chip sector</param>
        /// <param name="block">Block in sector</param>
        /// <returns>Success</returns>
        public bool Write(byte[] data, int sector, int block)
        {
            byte[] writeData = new byte[16];
            int copyBytes = data.Length;
            if(copyBytes > writeData.Length) copyBytes = writeData.Length;
            Array.Copy(data, writeData, copyBytes);

            var apduCommand = Commands.MifareClassicWriteCommand.Get(writeData, sector, block, _smartCard.GetReaderType());
            var response = _smartCard.Execute(apduCommand);
            return response.Success;
        }
    }
}