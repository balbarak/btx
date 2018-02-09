using System;
using System.Collections.Generic;
using System.Text;

namespace Btx.Cipher
{
    public class BtxEncryption
    {
        public static BtxData Encrypt(byte[] dataToEncrypt,MasterSecret master,byte[] publicKey)
        {
            BtxData result = new BtxData();

            var key = AesEncryption.GenerateNewKey();
            var encryptedKey = master.Encrypt(key,publicKey);

            var encryptedData = AesEncryption.Encrypt(dataToEncrypt, key);

            result.Key = Convert.ToBase64String(encryptedKey);
            result.Data = Convert.ToBase64String(encryptedData);
            
            return result;
        }

        public static BtxData Decrypt(BtxData data,MasterSecret master)
        {
            BtxData result = new BtxData();

            var encryptedKey = Convert.FromBase64String(data.Key);
            var encryptedData = Convert.FromBase64String(data.Data);

            var key = master.Decrypt(encryptedKey);

            var decryptedData = AesEncryption.Decrypt(encryptedData, key);


            result.Key = Convert.ToBase64String(key);
            result.Data = Convert.ToBase64String(decryptedData);

            return result;
        }
    }
}
