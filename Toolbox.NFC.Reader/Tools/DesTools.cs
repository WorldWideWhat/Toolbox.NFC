using System.Security.Cryptography;

namespace Toolbox.NFC.Reader.Tools
{
    public static class DesTools
    {
        public static byte[] Decrypt(byte[] input, byte[] key, byte[]? IV = null)
        {
            IV ??= new byte[8];
            using var tDes = TripleDES.Create();
            tDes.Key = key;
            tDes.Mode = CipherMode.CBC;
            tDes.Padding = PaddingMode.Zeros;
            tDes.IV = IV;
            return tDes.CreateDecryptor().TransformFinalBlock(input, 0, input.Length);
        }

        public static byte[] Encrypt(byte[] input, byte[] key, byte[]? IV = null)
        {
            IV ??= new byte[8];

            using var tDes = TripleDES.Create();
            tDes.Key = key;
            tDes.Mode = CipherMode.CBC;
            tDes.Padding = PaddingMode.Zeros;
            tDes.IV = IV;

            return tDes.CreateDecryptor().TransformFinalBlock(input, 0, input.Length);
        }

        public static byte[] CreateWriteKey(byte[] desKey)
        {
            if (desKey.Length != 16)
                throw new ArgumentException($"Invalid key length is {desKey.Length}, expected 16");

            var key1 = new byte[8];
            var key2 = new byte[8];
            var writekey = new byte[16];
            for(var pos = 0; pos < 8; pos++)
            {
                key1[pos] = desKey[7 - pos];
                key2[pos] = desKey[15 - pos];
            }
            Array.Copy(key1, 0, writekey, 0, key1.Length);
            Array.Copy(key2, 0, writekey, key1.Length, key2.Length);
            return writekey;
        }
    }
}
