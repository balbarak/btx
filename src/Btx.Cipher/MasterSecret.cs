using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Btx.Cipher
{
    public class MasterSecret
    {
        public byte[] PublicKey { get; private set; }
        private byte[] PrivateKey { get; set; }
        private string FilePath => Path.Combine(CipherConfig.DataFolder, CipherConfig.MASTER_SECRET_FILE_NAME);

        public MasterSecret()
        {
            Load();

            if (PrivateKey == null || PrivateKey.Length == 0)
            {
                AssignNewKey();
                Save();
            }
        }

        public void AssignNewKey()
        {
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                PublicKey = rsa.ExportCspBlob(false);
                PrivateKey = rsa.ExportCspBlob(true);
            }
        }

        public void Save()
        {
            var dir = CipherConfig.DataFolder;
            
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            var data = Convert.ToBase64String(PrivateKey);

            File.WriteAllText(FilePath, data);
        }

        public void Load()
        {
            if (!File.Exists(FilePath))
                return;

            var data = File.ReadAllText(FilePath);

            var keys = Convert.FromBase64String(data);

            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.ImportCspBlob(keys);
                PublicKey = rsa.ExportCspBlob(false);
                PrivateKey = rsa.ExportCspBlob(true);
            }
        }
    }
}
