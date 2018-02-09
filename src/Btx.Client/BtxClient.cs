using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Btx.Client
{
    
    public class BtxClient
    {
        private string _connectionUrl = "http://localhost:5000/btx";

        public delegate Task BtxConnectionEventHandler(object sender);
        public delegate Task BtxConnectionCloseEventHanlder(Exception ex);

        public event BtxConnectionEventHandler OnConnected;
        public event BtxConnectionCloseEventHanlder OnClosed;
        
        private HubConnection Connection { get; set; }

        public BtxClient()
        {
            Connection = new HubConnectionBuilder()
            .WithUrl(_connectionUrl)
            .WithConsoleLogger()
            .Build();

            SetupEvents();
        }

        public async Task Connect()
        {
            await Connection.StartAsync();
        }

        public async Task Disconnect()
        {
            await Connection.DisposeAsync();
        }

        private void SetupEvents()
        {
            Connection.Connected += OnConnectedInternal;
            Connection.Closed += OnClosedInternal;
        }

        private Task OnClosedInternal(Exception arg)
        {
            OnClosed?.Invoke(arg);

            return Task.FromResult(0);
        }

        private async Task OnConnectedInternal()
        {
            await OnConnected?.Invoke(this);
        }
        
    }
}
