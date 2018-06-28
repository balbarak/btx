using Btx.Client.Wpf.ViewModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Btx.Client.Wpf.Models
{
    public class LogEntry : BaseViewModel
    {
        public DateTime Date { get; set; }

        public string Message { get; set; }

        public LogLevel LogLevel { get; set; }
        
        public Brush LogColor
        {
            get
            {
                switch (LogLevel)
                {
                    case LogLevel.Trace:

                        return Brushes.Gray;

                    case LogLevel.Debug:

                        return Brushes.LightBlue;

                    case LogLevel.Information:

                        return Brushes.Blue;

                    case LogLevel.Warning:

                        return Brushes.Yellow;

                    case LogLevel.Error:

                        return Brushes.Red;

                    case LogLevel.Critical:

                        return Brushes.DarkRed;

                    case LogLevel.None:

                        return Brushes.Black;

                    default:
                        return Brushes.Black;
                }
            }
        }
        
    }
}
