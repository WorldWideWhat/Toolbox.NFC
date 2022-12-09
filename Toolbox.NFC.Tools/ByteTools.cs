using System.Security.Cryptography;

namespace Toolbox.NFC.Tools;
public static class ByteTools
{
    /// <summary>
    /// Get byte array filled with random data
    /// </summary>
    /// <param name="arraySize">Size of array</param>
    /// <returns>byte[]</returns>
    public static byte[] GenerateRandomArray(int arraySize)
    {
        using(var rng = RandomNumberGenerator.Create())
        {
            byte[] array = new byte[arraySize];
            rng.GetBytes(array);
            for(var i = 0; i < arraySize; i++)
            {
                byte keyByte = (byte)(array[i] & 0xFE);
                var parity = 0;
                for (var p = keyByte; p != 0; p >>= 1) parity ^= p & 1;
                array[i] = (byte)(keyByte | (parity == 0 ? 1 : 0));
            }
            return array;
        }
    }

    /// <summary>
    /// Rotate byte array left
    /// </summary>
    /// <param name="bytes">bytes</param>
    /// <returns>byte[]</returns>
    public static byte[] RotateArrayLeft(byte[] bytes)
    {
        var n_arr = new byte[bytes.Length];
        for(var i = 1; i < bytes.Length; i++)
        {
            n_arr[i-1] = bytes[i];
        }
        n_arr[n_arr.Length-1] = bytes[0];
        return n_arr;
    }

    /// <summary>
    /// Rotate byte array right
    /// </summary>
    /// <param name="bytes">bytes</param>
    /// <returns>byte[]</returns>
    public static byte[] RotateArrayRight(byte[] bytes)
    {
        var n_arr = new byte[bytes.Length];
        for(var i = 0; i < bytes.Length -1; i++)
        {
            n_arr[i + 1] = bytes[i];
        }
        n_arr[0] = bytes[bytes.Length-1];
        return n_arr;
    }
}
