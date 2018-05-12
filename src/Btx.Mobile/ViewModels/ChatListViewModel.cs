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

namespace Btx.Mobile.ViewModels
{
    public class ChatListViewModel : BaseViewModel
    {
        public ObservableRangeCollection<BtxThreadWrapper> Chats { get; private set; } = new ObservableRangeCollection<BtxThreadWrapper>();

        private BtxThreadWrapper _selectedItem;

        public BtxThreadWrapper SelectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value; OnPropertyChanged();}
        }
        
        public ChatListViewModel()
        {
            Title = "BTX Chat";
            
            LoadChats();

            //AddTestItem();
            
        }

        public void ChangeTitle(string title)
        {
            Title = title;
        }

        public Task GoToChatBox()
        {
            App.ChatManager.CurrentThread = SelectedItem;

            SelectedItem = null;

            return PushAsync(new ChatBoxPage());
        }
        
        public void LoadChats()
        {
            var chats = BtxThreadService.Instance.GetAll();

            foreach (var item in chats)
            {
                BtxThreadWrapper wrapper = new BtxThreadWrapper(item);
                Chats.Add(wrapper);
            }
        }
        
    }
}
