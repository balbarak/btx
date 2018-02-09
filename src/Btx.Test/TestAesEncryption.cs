using Btx.Cipher;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Text;

namespace Btx.Test
{
    [TestClass]
    public class TestAesEncryption
    {
        [TestMethod]
        public void TestEncrypt()
        {
            var key = AesEncryption.GenerateNewKey();

            string msg = "Hello World";
            var data = Encoding.UTF8.GetBytes(msg);

            var encryptedData = AesEncryption.Encrypt(data, key);


            var decryptedBytes = AesEncryption.Decrypt(encryptedData, key);
            var result = Encoding.UTF8.GetString(decryptedBytes);

            Assert.AreEqual(msg, result);

            
        }
    }
}
