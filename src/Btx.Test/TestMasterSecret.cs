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
    }
}
