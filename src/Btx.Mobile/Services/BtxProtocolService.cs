using Btx.Client;
using Btx.Client.Application.Services;
using Btx.Client.BtxEventsArg;
using Btx.Client.Domain.Models;
using Btx.Mobile.ViewModels;
using Btx.Mobile.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Btx.Mobile.Services
{
    public class BtxProtocolService
    {
        public static BtxProtocolService Instance { get; }

        private BtxClient _client;

        public BtxClient Client
        {
            get
            {
                if (_client == null)
                {
                    _client = new BtxClient();

                    SetupEvents();
                }

                return _client;
            }
            private set { _client = value; }
        }

        static BtxProtocolService()
        {
            Instance = new BtxProtocolService();
        }

        private void SetupEvents()
        {
            Client.OnConnected += OnConnected;
            Client.OnDisconnected += OnDisconnected;
            Client.OnMessageRecieved += OnMessageRecieved;
            Client.OnTokenRecieved += OnTokenRecieved;
           
        }
        
        private async void OnTokenRecieved(object sender, EventArgs e)
        {
            var token = e as TokenEventArgs;

            await Client.Connect();
        }

        private async void OnMessageRecieved(BtxMessage msg)
        {
            var thread = new BtxThread(msg);

            //thread = await BtxThreadService.Instance.AddOrUpdate(thread);

            //await BtxMessageService.Instance.Add(msg);

            var chatListViewModel = ServiceLocator.Current.GetService<ChatListViewModel>();

            chatListViewModel.Chats.Add(new BtxThreadWrapper(thread)
            {
                LastMessage = msg.Body,
                HasUnreadMessages = false,
                LastMessageDate = msg.Date.ToLocalTime(),
            });

        }

        private void OnDisconnected(object sender, EventArgs e)
        {

        }

        private void OnConnected(object sender, EventArgs e)
        {

        }
    }
}
