using Btx.Client.Domain.Models;
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

        private string _username;

        public string Username
        {
            get { return _username; }
            set { _username = value; OnPropertyChanged(); }
        }
        
        private string _password;

        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(); }
        }

        public LoginViewModel()
        {
            LoginCommand = new Command(async () => { await Login(); });
        }

        public async Task Login()
        {
            IsBusy = true;

            try
            {
                BtxLogin model = new BtxLogin()
                {
                    Password = this.Password,
                    Username = this.Username
                };

                await App.ChatManager.Client.Login(model);

                App.Instance.SetLoggedInPage();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Unable to login", ex.ToString(), "Ok");
            }

            IsBusy = false;
        }
    }
}
