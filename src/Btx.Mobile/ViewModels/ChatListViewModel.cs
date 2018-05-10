using Btx.Client.Domain.Models;
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

            AddTestItem();
            
        }

        public void ChangeTitle(string title)
        {
            Title = title;
        }

        public async Task GoToChatBox(ChatViewModel item)
        {
            await PushAsync(new ChatBoxPage(item));
        }


        private void AddTestItem()
        {
            var thread = new BtxThread()
            {
                Title = "Welcome",
            };

            Chats.Add(new BtxThreadWrapper(thread));
        }
    }
}
