using Toolbox.NFC.Reader.Commands;
using Toolbox.NFC.Reader.Tools;

namespace Toolbox.NFC.Reader.Card
{
    public class MifareUltralightC : MifareUltralight
    {

        public MifareUltralightC(SmartCard smartCard)
            :base(smartCard)
        {}


        public virtual bool Authorize(byte[] desKey)
        {
            var authInitCmd = new MifareUltralightCAuthInitCommand(_smartCard.GetReaderType());
            var initResp = _smartCard.Execute(authInitCmd);
            if (!initResp.Success) return false;
            
            if (initResp.ResponseData is null) return false;
            var rndA = ByteTools.GenerateRandomArray(8);
            var rndB1 = new byte[8];
            Array.Copy(initResp.ResponseData, 3, rndB1, 0, 8);
            var dRndB1 = DesTools.Decrypt(rndB1, desKey);
            dRndB1 = ByteTools.RotateArrayLeft(dRndB1);
            var rndB = ByteTools.RotateArrayLeft(DesTools.Decrypt(rndB1, desKey));

            var rndAB = new byte[rndA.Length + rndB.Length];
            Array.Copy(rndA, 0, rndAB, 0, rndA.Length);
            Array.Copy(rndB, 0, rndAB, rndA.Length, rndB.Length);

            var authKey = DesTools.Encrypt(rndAB, desKey, rndB1);

            var authCmd = new MifareUltralightCAuthorizeCommand(authKey, _smartCard.GetReaderType());
            var authResp = _smartCard.Execute(authCmd);
            if (!authResp.Success) return false;
            if (authResp.ResponseData is null) return false;

            var tstKey2 = new byte[8];
            Array.Copy(authKey, 7, tstKey2, 0, 8);
            var tstData = new byte[8];
            Array.Copy(authResp.ResponseData,3,tstData, 0, 8);
            var tstVal = ByteTools.RotateArrayRight(DesTools.Decrypt(tstData, desKey, tstKey2));
            for(int i=0; i < 8; i++)
            {
                if(tstVal[i] != rndA[i]) return false;
            }
            return true;
        }
    }
}
