using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Btx.Cipher
{
    public class RsaEncryption
    {
        public static byte[] Encrypt(byte[] dataToEncrypt,byte[] publicKey)
        {
            byte[] result = null;

            using (var rsa = new RSACryptoServiceProvider(CipherConfig.RSA_KEY_SIZE))
            {
                rsa.ImportCspBlob(publicKey);

                result = rsa.Encrypt(dataToEncrypt, true);
            }

            return result;
        }

        public static byte[] Decrypt(byte[] dataToEncrypt, byte[] privateKey)
        {
            byte[] result = null;

            using (var rsa = new RSACryptoServiceProvider(CipherConfig.RSA_KEY_SIZE))
            {
                rsa.ImportCspBlob(privateKey);

                result = rsa.Decrypt(dataToEncrypt, true);
            }

            return result;
        }
    }
}
