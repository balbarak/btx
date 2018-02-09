using System;
using System.Collections.Generic;
using System.Text;

namespace Btx.Cipher
{
    public class BtxData
    {
        public string Key { get; set; }

        public string Data { get; set; }

        public BtxData()
        {
            
        }

        public BtxData(string key,string data)
        {
            Key = key;
            Data = data;
        }
    }
}
