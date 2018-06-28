﻿using Btx.Client;
using Btx.Client.Application.Services;
using Btx.Client.BtxEventsArg;
using Btx.Client.Domain.Models;
using Btx.Mobile.Helpers;
using Btx.Mobile.ViewModels;
using Btx.Mobile.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

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
            msg.BtxMessageType = BtxMessageType.Incoming;

            var thread = new BtxThread(msg);

            thread = await BtxThreadService.Instance.AddOrUpdate(thread);

            msg.ThreadId = thread.Id;

            await BtxMessageService.Instance.Add(msg);

            AddMessageToChatList(msg);

            AddMessageToChatBox(msg, thread);

        }

        private void AddMessageToChatList(BtxMessage msg)
        {
            var chatListViewModel = ServiceLocator.Current.GetService<ChatListViewModel>();

            chatListViewModel.AddChatMessage(msg);
        }

        private void AddMessageToChatBox(BtxMessage msg, BtxThread thread)
        {
            if (CacheHelper.CurrenChatBoxViewModel != null && CacheHelper.CurrenChatBoxViewModel.BtxThread?.Id == thread.Id)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    CacheHelper.CurrenChatBoxViewModel.Items.Add(new BtxMessageWrapper(msg));
                });

            }
        }

        private void OnDisconnected(object sender, EventArgs e)
        {

        }

        private void OnConnected(object sender, EventArgs e)
        {

        }
    }
}