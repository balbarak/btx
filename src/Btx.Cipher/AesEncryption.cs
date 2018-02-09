using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Linq;

namespace Btx.Cipher
{
    public class AesEncryption
    {
        public static byte[] Encrypt(byte[] dataToEncrypt,byte[] key)
        {
            byte[] result = null;

            using (var aes = new AesCryptoServiceProvider())
            {
                byte[] iv = null;
                byte[] encryptedBytes = null;

                aes.Key = key;
                aes.GenerateIV();
                iv = aes.IV;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                encryptedBytes = encryptor.TransformFinalBlock(dataToEncrypt, 0, dataToEncrypt.Length);

                result = new byte[encryptedBytes.Length + iv.Length];

                Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
                Buffer.BlockCopy(encryptedBytes, 0, result, iv.Length, encryptedBytes.Length);
            }

            return result;

        }


        public static byte[] Decrypt(byte[] data,byte[] key)
        {
            byte[] result = null;

            using (AesManaged aesProvider = new AesManaged())
            {
                aesProvider.Key = key;
                aesProvider.IV = data.Skip(0).Take(16).ToArray();

                var dataToDecrypt = data.Skip(16).ToArray();

                ICryptoTransform decryptor = aesProvider.CreateDecryptor(aesProvider.Key, aesProvider.IV);
                result = decryptor.TransformFinalBlock(dataToDecrypt, 0, dataToDecrypt.Length);
            }

            return result;
        }

        public static byte[] GenerateNewKey(int lenght = 256)
        {
            byte[] result = new byte[lenght];

            using (var randomNumberGenerator = new RNGCryptoServiceProvider())
            {
                randomNumberGenerator.GetBytes(result);

                var hmac =  SHA256.Create();

                result = hmac.ComputeHash(result);
            }

            return result;
        }
    }
}
