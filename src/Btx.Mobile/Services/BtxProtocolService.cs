using Btx.Client;
using Btx.Client.Application;
using Btx.Client.Application.Persistance;
using Btx.Client.Application.Services;
using Btx.Client.BtxEventArgs;
using Btx.Client.BtxEventsArg;
using Btx.Client.Domain.Models;
using Btx.Client.Domain.Search;
using Btx.Mobile.Helpers;
using Btx.Mobile.ViewModels;
using Btx.Mobile.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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
        }

        public BtxThreadWrapper CurrentThread { get; set; }

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
            Client.OnMessageServerDelivered += OnMessageServerDelivered;
            Client.OnMessageUserDelivered += OnMessageUserDelivered;

        }

        private void OnMessageUserDelivered(object sender, EventArgs e)
        {
            if (e is BtxMessageEventArgs args)
            {
                var msg = BtxMessageService.Instance.GetById(args.MessageId);

                if (msg != null)
                {
                    msg.Status = BtxMessageStatus.UserDeliverd;

                    BtxMessageService.Instance.Update(msg);

                    if (CacheHelper.CurrenChatBoxViewModel.BtxThread.Id == msg.ThreadId)
                    {
                        CacheHelper.CurrenChatBoxViewModel.UpdateChatMessage(msg);
                    }
                }
            }
        }

        private void OnMessageServerDelivered(object sender, EventArgs e)
        {

            if (e is BtxMessageEventArgs args)
            {
                var msg = BtxMessageService.Instance.GetById(args.MessageId);

                if (msg != null)
                {
                    msg.Status = BtxMessageStatus.ServerDeliverd;

                    BtxMessageService.Instance.Update(msg);

                    if (CacheHelper.CurrenChatBoxViewModel.BtxThread.Id == msg.ThreadId)
                    {
                        CacheHelper.CurrenChatBoxViewModel.UpdateChatMessage(msg);
                    }
                }
            }
        }

        public async Task SendMessage(BtxMessage msg)
        {
            BtxMessageService.Instance.AddOrUpdate(msg);
            
            AddMessageToChatList(msg);

            await Client.Send(msg);
        }

        public async Task<SearchResult<BtxUser>> SearchBtxUser(BtxUserSearch search)
        {
            var result = await Client.SearchBtxUser(search);

            Task.Run(() =>
            {
                foreach (var item in result.Result)
                {
                    BtxUserService.Instance.AddOrUpdate(item);
                }
            });

            await Task.CompletedTask;

            return result;
        }

        private async void OnTokenRecieved(object sender, EventArgs e)
        {
            var token = e as TokenEventArgs;

            BtxSetting.DATABASE_FILE_NAME = $"{token.Username}.db";

            BtxDbContext.InitDatabase();

            await Client.Connect();
        }

        private async void OnMessageRecieved(BtxMessage msg)
        {

            msg.BtxMessageType = BtxMessageType.Incoming;

            var thread = new BtxThread(msg);

            thread = BtxThreadService.Instance.AddOrUpdate(thread);

            msg.ThreadId = thread.Id;

            if (AddMessageToChatBox(msg, thread))
                msg.IsReadByUser = true;
            else
                msg.IsReadByUser = false;
            
            BtxMessageService.Instance.AddOrUpdate(msg);
            
            AddMessageToChatList(msg);

            await Client.MessageDelivered(msg.Id);
            
        }

        public void AddThreadToChatList(BtxThread thread)
        {
            var chatListViewModel = ServiceLocator.Current.GetService<ChatListViewModel>();

            chatListViewModel.AddOrUpdateThread(thread);
        }

        private void AddMessageToChatList(BtxMessage msg)
        {
            var chatListViewModel = ServiceLocator.Current.GetService<ChatListViewModel>();

            chatListViewModel.AddChatMessage(msg);
            
            chatListViewModel.SortChats();

        }

        private bool AddMessageToChatBox(BtxMessage msg, BtxThread thread)
        {
            var result = false;

            if (CacheHelper.CurrenChatBoxViewModel != null && CacheHelper.CurrenChatBoxViewModel.BtxThread?.Id == thread.Id)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    CacheHelper.CurrenChatBoxViewModel.Items.Add(new BtxMessageWrapper(msg));

                    result = true;
                });

            }

            return result;
        }

        private bool IsCurrentChatBoxViewModelMatchThreadId(string threadId)
        {
            if (CacheHelper.CurrenChatBoxViewModel == null)
                return false;

            return CacheHelper.CurrenChatBoxViewModel.BtxThread?.Id == threadId;
        }
        
        private void OnDisconnected(object sender, EventArgs e)
        {

        }

        private async void OnConnected(object sender, EventArgs e)
        {
            await Client.GetPendingMessages();
        }
    }
}
