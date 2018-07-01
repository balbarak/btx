using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Btx.Client.Application
{
    public class BtxSetting
    {

        public static string DATABASE_FILE_NAME { get; set; } = "btxdb.db";

        public static string DATA_FOLDER_PATH
        {
            get
            {
                string personalFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                string path = Path.Combine(personalFolder, "data");

                return path;
                
            }
        }

        public static string DATABASE_FULL_FILE_PATH
        {
            get
            {
                return Path.Combine(DATA_FOLDER_PATH, DATABASE_FILE_NAME);
            }
        }
    }
}
