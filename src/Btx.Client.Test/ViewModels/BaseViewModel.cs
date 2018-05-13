using Btx.Client.Test.Helpers;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Btx.Client.Test.ViewModels
{
    public class BaseViewModel : MvvmHelpers.BaseViewModel
    {
        public BtxLoggerProvider LoggerProvider { get; set; } = new BtxLoggerProvider();

        public BaseViewModel()
        {

        }
    }
}
