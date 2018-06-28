using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Btx.Client.Domain.Models;
using Microsoft.AspNetCore.Http.Connections.Client;
using Microsoft.Extensions.Options;
using System.Net.Http;
using Newtonsoft.Json;
using Btx.Client.Exceptions;
using Btx.Client.Domain;
using Microsoft.AspNetCore.SignalR.Protocol;
using Btx.Client.BtxEventsArg;
using Btx.Client.Domain.Search;
using Btx.Client.BtxEventArgs;

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
        public event BtxMessageEventHandler OnMessageRecieved;
        public event EventHandler OnTokenRecieved;
        public event EventHandler OnMessageServerDelivered;

        public bool IsConnected { get; private set; }

        public BtxClient()
        {

        }

        public BtxClient(ILoggerProvider loggerProvider)
        {
            _loggerProvider = loggerProvider;
            _logger = loggerProvider.CreateLogger("Client");
        }

        public async Task Connect()
        {
            if (String.IsNullOrWhiteSpace(_accessToken))
                throw new BtxClientException("no access token to login");

            try
            {
                SetupConnection();

                await _hubConnection.StartAsync(_ctk).ConfigureAwait(false);

                IsConnected = true;

                OnConnected?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                _logger?.LogError($"Unable to coneect: {ex}");

                IsConnected = false;
            }

        }

        public async Task Disconnect()
        {
            IsConnected = false;

            if (_hubConnection != null)
                await _hubConnection.DisposeAsync();
        }

        public async Task Send(BtxMessage msg)
        {
            await _hubConnection.InvokeAsync<BtxMessage>("Send", msg);
        }

        public async Task Register(BtxRegister model)
        {

            try
            {
                _logger?.LogInformation("Begin registering ...");

                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Config.BTX_API_BASE_URL);

                    var jsonModel = JsonConvert.SerializeObject(model);

                    StringContent content = new StringContent(jsonModel, Encoding.UTF8, _jsonContentType);

                    var response = await client.PostAsync("register", content);

                    var result = await response.Content.ReadAsStringAsync();

                    if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        _logger?.LogInformation("Unable to register");
                        throw new BtxClientException(result);
                    }

                    _logger?.LogInformation("register success. reading token ...");

                    _accessToken = result;

                    RemoveExtraFromToken();

                    _logger?.LogInformation($"access toke: {_accessToken}");

                    OnTokenRecieved?.Invoke(this, new TokenEventArgs(_accessToken));
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError("Unable to login error: {0}", ex);

                throw ex;
            }

        }

        public async Task Login(BtxLogin model)
        {

            try
            {
                _logger?.LogInformation("Begin login ...");

                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Config.BTX_API_BASE_URL);

                    var jsonModel = JsonConvert.SerializeObject(model);

                    StringContent content = new StringContent(jsonModel, Encoding.UTF8, _jsonContentType);

                    var response = await client.PostAsync("login", content).ConfigureAwait(false);

                    var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                    if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        _logger?.LogInformation("Unable to login");
                        throw new BtxClientException(result);
                    }

                    _logger?.LogInformation("login success. reading token ...");

                    _accessToken = result.Trim();

                    RemoveExtraFromToken();

                    _logger?.LogInformation($"access token: {_accessToken}");

                    OnTokenRecieved?.Invoke(this, new TokenEventArgs(_accessToken));
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError("Unable to register error: {0}", ex);

                throw ex;
            }

        }

        public async Task<SearchResult<BtxUser>> SearchUsers()
        {
            SearchResult<BtxUser> result;

            try
            {
                if (string.IsNullOrWhiteSpace(_accessToken))
                    throw new Exception("There is no access token");

                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Config.BTX_API_BASE_URL);

                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_accessToken}");

                    var response = await client.GetAsync("BtxUser").ConfigureAwait(false);

                    var data = await response.Content.ReadAsStringAsync();

                    result = JsonConvert.DeserializeObject<SearchResult<BtxUser>>(data);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Unable to search error: {0}", ex);

                throw ex;
            }

            return result;
        }

        public void SetupConnection()
        {
            var httpOptions = new HttpConnectionOptions
            {
                Url = new Uri(Config.BTX_URL),
            };

            httpOptions.Headers.Add("Authorization", $"Bearer {_accessToken}");

            var loggerFactory = new LoggerFactory();

            if (_loggerProvider != null)
                loggerFactory.AddProvider(_loggerProvider);

            var options = Options.Create(httpOptions);

            HttpConnectionFactory factory = new HttpConnectionFactory(options, loggerFactory);

            JsonHubProtocol jsonProtocol = new JsonHubProtocol();

            _hubConnection = new HubConnection(factory, jsonProtocol, loggerFactory);

            SetupEvents();
        }

        private Task InternalClosed(Exception arg)
        {
            IsConnected = false;

            OnDisconnected?.Invoke(this, EventArgs.Empty);

            return Task.FromResult(0);
        }

        private void RemoveExtraFromToken()
        {
            _accessToken = _accessToken.Replace("\"", "");
        }

        private void SetupEvents()
        {
            _hubConnection.Closed += InternalClosed;

            _hubConnection.On<BtxMessage>(ClientMethods.ON_MESSAGE_RECIEVE, (msg) =>
            {
                OnMessageRecieved?.Invoke(msg);
            });

            _hubConnection.On<string>(ClientMethods.ON_MESSAGE_SERVER_DELIEVERED, (msgId) =>
             {
                 OnMessageServerDelivered?.Invoke(this, new BtxMessageEventArgs() { MessageId = msgId });
             });
        }

    }
}
