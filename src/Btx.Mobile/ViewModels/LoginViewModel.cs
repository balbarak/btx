using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Btx.Mobile.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public ICommand LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new Command(async () => { Login(); });
        }

        public async Task Login()
        {
            App.Instance.SetLoginPage();
        }
    }
}
