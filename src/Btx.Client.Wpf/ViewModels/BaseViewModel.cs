using Btx.Client.Wpf.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Btx.Client.Wpf.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public BtxLoggerProvider LoggerProvider { get; set; } = new BtxLoggerProvider();
        
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
