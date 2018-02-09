using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Btx.Cipher
{
    public class CipherConfig
    {
        public static string DataFolder => $"{Path.Combine(Directory.GetCurrentDirectory(),"data")}";

        public const string MASTER_SECRET_FILE_NAME = "master.sec";
    }
}
