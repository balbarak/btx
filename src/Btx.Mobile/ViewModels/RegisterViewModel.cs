using Btx.Mobile.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Btx.Mobile.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        private UsernameValidation<string> _username;

        public UsernameValidation<string> Username
        {
            get { return _username; }
            set { _username = value; OnPropertyChanged(); }
        }

        private string _nickname;

        public string Nickname
        {
            get { return _nickname; }
            set { _nickname = value; OnPropertyChanged(); }
        }

        private string _password;

        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(); }
        }

        public ICommand RegisterCommand { get; }

        public RegisterViewModel()
        {
            RegisterCommand = new Command(async () =>
            {
                await Register();
            });
        }

        public async Task Register()
        {
            
            IsBusy = true;
           
            await Task.Delay(5000);

            IsBusy = false;
        }
    }
}
