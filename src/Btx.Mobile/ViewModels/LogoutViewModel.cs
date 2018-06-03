using Btx.Mobile.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Btx.Mobile.ViewModels
{
    public class LogoutViewModel : BaseViewModel
    {
        public ICommand GoToRegisterPageCommand { get; }

        public ICommand GoToLoginPageCommand { get; }

        public LogoutViewModel()
        {
            GoToRegisterPageCommand = new Command(async () => { await GoToRegisterPage(); });

            GoToLoginPageCommand = new Command(async () => { await GoToLoginPage(); });
        }

        public async Task GoToRegisterPage()
        {
            await PushAsync(new RegisterPage());
        }

        public async Task GoToLoginPage()
        {
            await PushAsync(new LoginPage());
        }
    }
}
