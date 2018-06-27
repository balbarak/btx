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

namespace Btx.Mobile.ViewModels
{
    public class ChatListViewModel : BaseViewModel
    {
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
        }

        public override async Task OnAppearing()
        {
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
            App.ChatManager.CurrentThread = SelectedItem;

            SelectedItem = null;

            await PushAsync(new ChatBoxPage(), true);
        }

        public async Task LoadThreads()
        {
            Chats.Clear();
            
            IsBusy = true;

            var chats = await BtxThreadService.Instance.GetAll();

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

            IsBusy = false;

        }

        public void AddChatMessage(BtxMessage msg)
        {
            var found = Chats.Where(a => a.Id == msg.RecipientId).FirstOrDefault();

            if (found != null)
            {
                found.SetMessageData(msg);
            }
            else
            {
                BtxThreadWrapper thread = new BtxThreadWrapper(new BtxThread(msg), msg);
                Chats.Add(thread);
            }
        }

    }

}

