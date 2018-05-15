using Btx.Client.Domain.Models;
using Btx.Client.Exceptions;
using Btx.Client.Test.Helpers;
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
        }

        private void SetupEvents()
        {
            Client.OnConnected += OnConnected;
            Client.OnDisconnected += OnDisconnected;
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

                Registeration model = new Registeration()
                {
                    Nickname = "looksharp",
                    Username = "Fuckoff",
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

    }
}
