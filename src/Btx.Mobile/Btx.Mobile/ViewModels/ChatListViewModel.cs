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
        public ObservableRangeCollection<Chat> Chats { get; set; } = new ObservableRangeCollection<Chat>();
        
        public ChatListViewModel()
        {
            Chats.AddRange(ChatMockService.GetChats());
            
        }

        public async Task GoToChatBox(Chat item)
        {
            await PushAsync(new ChatBoxPage(item));
        }
    }
}
