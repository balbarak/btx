using Btx.Client.Wpf.Helpers;
using Btx.Client.Wpf.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Btx.Client.Wpf.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public ICommand ShowClientWindowCommand { get; } = new RelayCommand((arg) =>
        {
            ClientWindow window = new ClientWindow();
            window.Show();
        });
    }
}
