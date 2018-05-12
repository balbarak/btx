using Btx.Client.Domain.Models;
using Btx.Mobile.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Btx.Mobile.Helpers
{
    public class CacheHelper
    {
        public static BtxThreadWrapper CurrentThread { get; set; }
    }
}
