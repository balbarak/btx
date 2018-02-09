using Btx.Cipher;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Btx.Test
{
    [TestClass]
    public class TestMasterSecret
    {
        [TestMethod]
        public void TestLoad()
        {
            MasterSecret master = new MasterSecret();
         
            byte[] currentPublicKey = new byte[master.PublicKey.Length];

            master.PublicKey.CopyTo(currentPublicKey, 0);

            master.Load();

            if (master.PublicKey.Length != currentPublicKey.Length)
                Assert.Fail();

            for (int i = 0; i < master.PublicKey.Length; i++)
            {
                if (!currentPublicKey[i].Equals(master.PublicKey[i]))
                    Assert.Fail();
            }

            
        }

        [TestMethod]
        public void TestEncrypt()
        {
            MasterSecret master = new MasterSecret();

            var key = AesEncryption.GenerateNewKey();

            var encryptedKey = master.Encrypt(key, master.PublicKey);

            var decryptedKey = master.Decrypt(encryptedKey);


            if (key.Length != decryptedKey.Length)
                Assert.Fail();

            for (int i = 0; i < decryptedKey.Length; i++)
            {
                if (!decryptedKey[i].Equals(key[i]))
                    Assert.Fail();
            }
        }
    }
}
