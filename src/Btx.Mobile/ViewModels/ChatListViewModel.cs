using Btx.Client.Application.Services;
using Btx.Client.Domain.Models;
using Btx.Mobile.Helpers;
using Btx.Mobile.MockData;
using Btx.Mobile.Models;
using Btx.Mobile.Views;
using Btx.Mobile.Wrappers;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;
using System.Collections.Specialized;
using System.Threading;
using System.Diagnostics;
using Btx.Mobile.Services;

namespace Btx.Mobile.ViewModels
{
    public class ChatListViewModel : BaseViewModel
    {
        private bool _isThreadLoaded;

        private readonly ReaderWriterLockSlim _itemsLock = new ReaderWriterLockSlim();

        public ObservableRangeCollection<BtxThreadWrapper> Chats { get; private set; } = new ObservableRangeCollection<BtxThreadWrapper>();

        private BtxThreadWrapper _selectedItem;

        public BtxThreadWrapper SelectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value; OnPropertyChanged(); }
        }

        public ChatListViewModel()
        {
            Title = "BTX Chat";

            Chats.CollectionChanged += OnCollectionChanged;
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {

        }

        public override async Task OnAppearing()
        {
            if (!_isThreadLoaded)
                await LoadThreads();
        }

        public override Task OnDisappearing()
        {
            return base.OnDisappearing();
        }

        public void SetupEvents()
        {

        }

        public void ChangeTitle(string title)
        {
            Title = title;
        }

        public async Task GoToChatBox()
        {
            BtxProtocolService.Instance.CurrentThread = SelectedItem;

            SelectedItem = null;

            await PushAsync(new ChatBoxPage(), true);
        }

        public async Task LoadThreads()
        {
            IsBusy = true;

            var chats = BtxThreadService.Instance.GetAll();

            foreach (var item in chats)
            {
                BtxThreadWrapper wrapper = new BtxThreadWrapper(item);

                var lastMessage = BtxMessageService.Instance.GetLastMessageByThreadId(item.Id);

                if (lastMessage != null)
                {
                    wrapper.LastMessage = lastMessage.Body;
                    wrapper.LastMessageDate = lastMessage.Date;
                }

                Chats.Add(wrapper);
            }

            SortChats();

            _isThreadLoaded = true;

            await Task.CompletedTask;

            IsBusy = false;

        }

        public void AddChatMessage(BtxMessage msg)
        {
            try
            {
                var found = Chats.Where(a => a.Id == msg.RecipientId).FirstOrDefault();

                Device.BeginInvokeOnMainThread(() =>
                {
                    if (found != null)
                    {
                        found.SetMessageData(msg);
                    }
                    else
                    {
                        BtxThreadWrapper thread = new BtxThreadWrapper(new BtxThread(msg), msg);
                        Chats.Add(thread);
                    }
                });

            }
            catch (Exception)
            {
                Debug.WriteLine("Add chats error");
            }

        }

        public void AddOrUpdateThread(BtxThread thread)
        {
            try
            {
                var found = Chats.Where(a => a.Id == thread.Id).FirstOrDefault();

                Device.BeginInvokeOnMainThread(() =>
                {
                    if (found != null)
                    {
                        found.UpdateThread(found.Model);
                    }
                    else
                    {
                        BtxThreadWrapper newThread = new BtxThreadWrapper(thread);
                        Chats.Add(newThread);
                    }
                });

            }
            catch (Exception)
            {
                Debug.WriteLine("Add chats error");
            }
        }

        public void SortChats()
        {
            try
            {

                var sortedItems = Chats.OrderByDescending(a => a.LastMessageDate).ToList();

                foreach (var item in sortedItems)
                {
                    var newIndex = sortedItems.IndexOf(item);
                    var oldIndex = Chats.IndexOf(item);

                    Chats.Move(oldIndex, newIndex);
                }
            }
            catch
            {
                Debug.WriteLine("Sort chats error");
            }
        }

        public void ReadAllMessages(string threadId)
        {
            var found = Chats.Where(a => a.Id == threadId).FirstOrDefault();

            if (found != null)
            {
                found.HasUnreadMessages = false;
                found.UnreadMessageCount = 0;
            }
        }

    }

}

