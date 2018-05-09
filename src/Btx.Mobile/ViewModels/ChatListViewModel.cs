using Btx.Mobile.MockData;
using Btx.Mobile.Models;
using Btx.Mobile.Views;
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
        public ObservableRangeCollection<ChatViewModel> Chats { get; } = App.ChatManager.ChatViewModels;

        public ChatListViewModel()
        {
            Title = "BTX Chat";
            
        }

        public void ChangeTitle(string title)
        {
            Title = title;
        }

        public async Task GoToChatBox(ChatViewModel item)
        {
            await PushAsync(new ChatBoxPage(item));
        }
    }
}
