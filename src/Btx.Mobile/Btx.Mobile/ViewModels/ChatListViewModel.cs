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
        public ObservableRangeCollection<Chat> Chats { get; } = App.ChatManager.Chats;

        public ChatListViewModel()
        {
            Title = "BTX Chat";

        }

        public void ChangeTitle(string title)
        {
            Title = title;
        }

        public async Task GoToChatBox(Chat item)
        {
            await PushAsync(new ChatBoxPage(item));
        }
    }
}
