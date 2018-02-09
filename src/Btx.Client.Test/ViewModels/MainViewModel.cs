using Btx.Client.Test.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Btx.Client.Test.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public BtxClient Client { get; set; } = new BtxClient();

        public ICommand ConnectCommand { get; set; }

        public ICommand DisconnectCommand { get; set; }

        private string log = "";
        public string Log
        {
            get { return log; }
            set { log = value; OnPropertyChanged(); }
        }

        private bool isConnected;
        public bool IsConnected
        {
            get { return isConnected; }
            set { isConnected = value; OnPropertyChanged(); }
        }


        public MainViewModel()
        {
            ConnectCommand = new DelegateCommand(async (data) => { await Connect(); });
            DisconnectCommand = new DelegateCommand(async (data) => { await Disconnect(); });

            Client.OnConnected += OnConnected;
            Client.OnClosed += OnClosed;
        }

        private Task OnClosed(Exception ex)
        {
            AddLog($"Disconnected: {ex?.ToString()}");

            IsConnected = false;

            return Task.FromResult(0);

        }

        public Task OnConnected(object sender)
        {
            AddLog("Connected");
            IsConnected = true;
            return Task.FromResult(0);
        }

        public async Task Connect()
        {
            AddLog("Connecting ...");

            if (!IsConnected)
                await Client.Connect();
        }

        public async Task Disconnect()
        {
            await Client.Disconnect();
        }

        public void AddLog(string text)
        {
            Log += $"{text}\n";
        }
    }
}
