using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using Btx.Client.Domain.Models;
using Microsoft.AspNetCore.Http.Connections.Client;
using Microsoft.AspNetCore.SignalR.Protocol;
using Microsoft.Extensions.Options;
using System.Net.Http;
using Newtonsoft.Json;
using Btx.Client.Exceptions;

namespace Btx.Client
{
    public class BtxClient
    {
        private HubConnection _hubConnection;
        private CancellationToken _ctk = new CancellationToken();
        private ILoggerProvider _loggerProvider;
        private ILogger _logger;
        private string _accessToken;
        private string _jsonContentType = "application/json";

        public event EventHandler OnDisconnected;
        public event EventHandler OnConnected;

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

                OnConnected?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unable to coneect: {ex}");

                IsConnected = false;
            }



        }

        public async Task Send()
        {
            await _hubConnection.InvokeAsync<BtxMessage>("Send");
        }

        public async Task Register(Registeration model)
        {

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Config.BTX_API_BASE_URL);

                    var jsonModel = JsonConvert.SerializeObject(model);

                    StringContent content = new StringContent(jsonModel, Encoding.UTF8, _jsonContentType);

                    var response = await client.PostAsync("register", content);
                    
                    var result = await response.Content.ReadAsStringAsync();

                    if (response.StatusCode != System.Net.HttpStatusCode.OK)
                        throw new BtxClientException(result);

                    _accessToken = result;
                }
            }
            catch (Exception ex)
            {
                throw new BtxClientException("Unable to register", ex);
            }

        }

        private void SetupConnection()
        {
            var httpOptions = new HttpConnectionOptions
            {
                Url = new Uri(Config.BTX_URL)
            };

            var loggerFactory = new LoggerFactory();

            if (_loggerProvider != null)
                loggerFactory.AddProvider(_loggerProvider);

            var options = Options.Create(httpOptions);

            HttpConnectionFactory factory = new HttpConnectionFactory(options, loggerFactory);

            JsonHubProtocol jsonProtocol = new JsonHubProtocol();

            _hubConnection = new HubConnection(factory, jsonProtocol, loggerFactory);

            _hubConnection.Closed += InternalClosed;
        }

        private Task InternalClosed(Exception arg)
        {
            IsConnected = false;

            OnDisconnected?.Invoke(this, EventArgs.Empty);

            return Task.FromResult(0);
        }

    }
}
