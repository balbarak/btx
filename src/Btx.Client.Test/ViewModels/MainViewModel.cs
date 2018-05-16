using Btx.Client.Domain.Models;
using Btx.Client.Exceptions;
using Btx.Client.Test.Helpers;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Btx.Client.Test.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public BtxClient Client { get; private set; }

        public ICommand ConnectCommand { get; }

        public ICommand RegisterCommand { get; }

        public ICommand LoginCommand { get; }

        public ICommand SendCommand { get; }
        
        private string _username = "user";

        public string Username
        {
            get { return _username; }
            set { _username = value; OnPropertyChanged(); }
        }

        private string _nickname = "looksharp";

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

        private string _loginUsername = "user";

        public string LoginUsername
        {
            get { return _loginUsername; }
            set { _loginUsername = value; OnPropertyChanged(); }
        }

        private string _toUserId;

        public string ToUserId
        {
            get { return _toUserId; }
            set { _toUserId = value; OnPropertyChanged(); }
        }

        private string _messageToSend;

        public string MessageToSend
        {
            get { return _messageToSend; }
            set { _messageToSend = value; OnPropertyChanged(); }
        }


        public bool IsDisconnected
        {
            get
            {
                
                return !Client.IsConnected;
            }
        }

        public string LogMessages
        {
            get
            {
                return LoggerProvider.CurrentLogger.LogMessages;
            }
        }

        public MainViewModel()
        {

            Client = new BtxClient(LoggerProvider);
            SetupEvents();

            LoggerProvider.CurrentLogger.OnWriteLog += CurrentLogger_OnWriteLog;

            ConnectCommand = new RelayCommand(
                async (obj) =>
                {
                    await Connect();
                });

            RegisterCommand = new RelayCommand(async (obj) =>
            {
                await Register();
            });

            LoginCommand = new RelayCommand(async (obj) =>
            {
                await Login();
            });

            SendCommand = new RelayCommand(async (obj) =>
            {
                await Send();
            });
        }

        private void SetupEvents()
        {
            Client.OnConnected += OnConnected;
            Client.OnDisconnected += OnDisconnected;
            Client.OnMessageRecieved += OnMessageRecieved;
        }

        private void OnMessageRecieved(BtxMessage msg)
        {
            LoggerProvider.CurrentLogger.LogInformation(msg.Body);
        }

        private void OnDisconnected(object sender, EventArgs e)
        {
            OnPropertyChanged(nameof(IsDisconnected));
        }

        private void OnConnected(object sender, EventArgs e)
        {
            OnPropertyChanged(nameof(IsDisconnected));
        }

        private void CurrentLogger_OnWriteLog(object sender, EventArgs e)
        {
            OnPropertyChanged(nameof(LogMessages));
        }

        private async Task Connect()
        {
            await Client.Connect();
        }

        private async Task Register()
        {
            try
            {
                IsBusy = true;

                BtxRegister model = new BtxRegister()
                {
                    Nickname = Nickname,
                    Username = Username,
                    Password = "1122"
                };

                await Client.Register(model);

                
            }
            catch (BtxClientException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                IsBusy = false;
            }
            
        }

        private async Task Login()
        {
            try
            {
                IsBusy = true;

                BtxLogin model = new BtxLogin()
                {
                    Username = LoginUsername,
                    Password = "1122"
                };

                await Client.Login(model);


            }
            catch (BtxClientException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                IsBusy = false;
            }

        }

        private async Task Send()
        {
            try
            {
                IsBusy = true;

                BtxMessage msg = new BtxMessage()
                {
                    Body = MessageToSend,
                    ToUserId = ToUserId
                };

                await Client.Send(msg);


            }
            catch (BtxClientException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                IsBusy = false;
            }

        }
    }
}
