using Btx.Client.Wpf.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Btx.Client.Wpf.AppEventArgs
{
    public class LogEventArgs : EventArgs
    {
        public LogEntry LogEntry { get; set; }
    }
}
