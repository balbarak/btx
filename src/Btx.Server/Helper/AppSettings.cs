using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Btx.Server.Helper
{
    public class AppSettings
    {
        public static IConfiguration Configuration { get; set; }
    }
}
