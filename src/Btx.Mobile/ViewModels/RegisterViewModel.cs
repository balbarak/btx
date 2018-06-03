using Btx.Client.Domain.Models;
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
        private string _username;

        public string Username
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

            var model = new BtxRegister()
            {
                Nickname = this.Nickname,
                Password = this.Password,
                Username = this.Username
            };

            try
            {
                await App.ChatManager.Client.Register(model);

                App.Instance.SetLoggedInPage();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Unable to register", ex.ToString(), "Ok");
            }
            

            IsBusy = false;
        }
    }
}
