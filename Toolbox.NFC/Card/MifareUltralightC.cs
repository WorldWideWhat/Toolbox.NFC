using System;
using Toolbox.NFC.Tools;

namespace Toolbox.NFC.Card
{
    public class MifareUltralightC : MifareUltralight
    {
        /// <summary>
        /// Default mifare ultralight C key
        /// </summary>
        public static readonly byte[] DefaultKey = new byte[] { 0x49, 0x45, 0x4D, 0x4B, 0x41, 0x45, 0x52, 0x42, 0x21, 0x4E, 0x41, 0x43, 0x55, 0x4F, 0X59, 0x46 };
        public MifareUltralightC(SmartCard smartCard)
            :base(smartCard)
        {
            if (_smartCard.GetReaderType() != Reader.Driver.ReaderType.Omnikey)
                throw new Exception("Connected reader not supported");
        }

        /// <summary>
        /// Authorize card
        /// </summary>
        /// <param name="desKey">Triple DES key</param>
        /// <returns>Success</returns>
        public virtual bool Authorize(byte[] desKey)
        {
            var adpuInit = new Commands.MifareUltralightCAuthInitCommand(_smartCard.GetReaderType());
            var response = _smartCard.Execute(adpuInit, adpuInit.GetBuffer().Length+1);
            
            if (!response.Success) return false;

            if(response.ResponseData.Length < 11) return false;
            var rndB_1 = new byte[8];
            Array.Copy(response.ResponseData, 3, rndB_1, 0, 8);

            var rndBn = DesTools.Decrypt(rndB_1, desKey);
            var rndB = ByteTools.RotateArrayLeft(rndBn);

            var rndA_1 = ByteTools.GenerateRandomArray(8);
            var rndAB = new byte[rndA_1.Length + rndB.Length];
            Array.Copy(rndA_1, 0, rndAB, 0, rndA_1.Length);
            Array.Copy(rndB, 0, rndAB, rndA_1.Length, rndB.Length);

            var authKey2 = DesTools.Encrypt(rndAB, desKey, rndB_1);
            var testKey2 = new byte[8];
            Array.Copy(authKey2, 8, testKey2 , 0, 8);

            var apduAuth = new Commands.MifareUltralightCAuthorizeCommand(authKey2, _smartCard.GetReaderType());
            response = _smartCard.Execute(apduAuth, apduAuth.GetBuffer().Length+1); 
            if (!response.Success) return false;
            if (response.ResponseData.Length < 11) return false;

            var authData = new byte[8];
            Array.Copy(response.ResponseData, 3, authData , 0, 8);

            var testVal = DesTools.Decrypt(authData, desKey, testKey2);
            testVal = ByteTools.RotateArrayRight(testVal);
            for(var index =0; index < testVal.Length;index++)
            {
                if (testVal[index] != rndA_1[index]) return false;
            }
            return true;
        }

        /// <summary>
        /// Change key
        /// </summary>
        /// <param name="desKey">New Triple DES key</param>
        /// <returns>Success</returns>
        public virtual bool ChangeKey(byte[] desKey)
        {
            var newKey = DesTools.CreateWriteKey(desKey);
            var writePage = 0x2c;
            for (int cnt = 0; cnt < newKey.Length; cnt += 4)
            {
                var page = new byte[] { newKey[cnt], newKey[cnt + 1], newKey[cnt + 2], newKey[cnt + 3] };
                if (!WritePage(writePage, page)) return false;
                writePage++;
            }
            return true;
        }
    }
}
