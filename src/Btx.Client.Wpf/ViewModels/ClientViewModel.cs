using Btx.Client.BtxEventsArg;
using Btx.Client.Domain.Models;
using Btx.Client.Wpf.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Btx.Client.Wpf.ViewModels
{
    public class ClientViewModel : BaseViewModel
    {
        private BtxClient _client;

        public BtxClient Client
        {
            get
            {
                return _client;
            }
            set { _client = value; }
        }

        public bool IsConnected { get { return Client.IsConnected; } }

        public bool IsDisconnected
        {
            get
            {
                return !IsConnected;
            }
        }

        private string _username;

        public string Username
        {
            get { return _username; }
            set { _username = value; RaisePropertyChanged(); }
        }

        private string _password;

        public string Password
        {
            get { return _password; }
            set { _password = value; RaisePropertyChanged(); }
        }

        private string _nickname;

        public string Nickname
        {
            get { return _nickname; }
            set { _nickname = value; RaisePropertyChanged(); }
        }

        private ICommand _loginCommand;

        public ICommand LoginCommand
        {
            get
            {
                if (_loginCommand == null)
                    _loginCommand = new RelayCommand(async (arg) => { await Login(); });

                return _loginCommand;
            }
        }

        private ICommand _registerCommand;

        public ICommand RegisterCommand
        {
            get
            {
                if (_registerCommand != null)
                    _registerCommand = new RelayCommand(async (args) => { await Register(); });
                return _registerCommand;
            }
            set { _registerCommand = value; }
        }

        private ICommand _connectCommand;

        public ICommand ConnectCommand
        {
            get
            {
                if (_connectCommand == null)
                    _connectCommand = new RelayCommand(async (args) => { await Connect(); });

                return _connectCommand;
            }
        }

        private ICommand _disconnectCommand;

        public ICommand DisconnectCommand
        {
            get
            {
                if (_disconnectCommand == null)
                    _disconnectCommand = new RelayCommand(async (args) => { await Client.Disconnect(); });

                return _disconnectCommand;
            }
        }


        private string _token;

        public bool HasToken { get { return !string.IsNullOrWhiteSpace(Token); } }

        public bool NoToken { get { return string.IsNullOrWhiteSpace(Token); } }

        public string Token
        {
            get { return _token; }
            set
            {
                _token = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(NoToken));
                RaisePropertyChanged(nameof(HasToken));
            }
        }

        public string Log
        {
            get { return LoggerProvider.CurrentLogger.LogMessages; }

        }

        public ClientViewModel()
        {
            _client = new BtxClient(LoggerProvider);

            LoggerProvider.CurrentLogger.OnWriteLog += OnLog;

            SetupEvents();
        }

        private void SetupEvents()
        {
            Client.OnConnected += OnConnected;
            Client.OnDisconnected += OnDisconnected;
            Client.OnTokenRecieved += OnTokenRecieved;
        }

        private void OnDisconnected(object sender, EventArgs e)
        {
            RaisePropertyChanged(nameof(IsConnected));
            RaisePropertyChanged(nameof(IsDisconnected));
        }

        private void OnConnected(object sender, EventArgs e)
        {
            RaisePropertyChanged(nameof(IsConnected));
            RaisePropertyChanged(nameof(IsDisconnected));
        }

        private void OnTokenRecieved(object sender, EventArgs e)
        {
            var token = e as TokenEventArgs;

            this.Token = token?.Token;

        }

        private async Task Login()
        {
            BtxLogin model = new BtxLogin()
            {
                Username = this.Username,
                Password = this.Password
            };

            try
            {
                await Client.Login(model);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Unable to loign", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        public async Task Register()
        {
            BtxRegister model = new BtxRegister()
            {
                Nickname = this.Nickname,
                Username = this.Username,
                Password = this.Password
            };

            try
            {
                await Client.Register(model);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Unable to register", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public async Task Connect()
        {
            try
            {
                await Client.Connect();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Unable to connect", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
        private void OnLog(object sender, EventArgs e)
        {
            RaisePropertyChanged(nameof(Log));
        }
    }
}
