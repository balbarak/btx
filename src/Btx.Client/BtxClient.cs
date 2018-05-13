using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace Btx.Client
{
    public class BtxClient
    {
        private HubConnection _hubConnection;
        private CancellationToken _ctk = new CancellationToken();
        private ILoggerProvider _loggerProvider;
        private ILogger _logger;

        public bool IsConnected { get; private set; }
        
        public BtxClient()
        {
            SetupConnection();
        }

        public BtxClient(ILoggerProvider loggerProvider) 
        {
            _loggerProvider = loggerProvider;
            _logger = loggerProvider.CreateLogger("Client");

            SetupConnection();
        }
        
        public async Task Connect()
        {
            try
            {
                await _hubConnection.StartAsync(_ctk);

                IsConnected = true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unable to coneect: {ex}");
                
                IsConnected = false;
            }
            
            
            
        }

        private void SetupConnection()
        {
            HubConnectionBuilder builder = new HubConnectionBuilder();
            _hubConnection = builder
                .WithUrl(Config.BTX_URL)
                .ConfigureLogging((logger) =>
                {
                    if (_loggerProvider != null)
                        logger.AddProvider(_loggerProvider);
                    
                })
                .Build();
            _hubConnection.Closed += InternalClosed;
        }
        
        private Task InternalClosed(Exception arg)
        {
            IsConnected = false;

            return Task.FromResult(0);
        }

    }
}
