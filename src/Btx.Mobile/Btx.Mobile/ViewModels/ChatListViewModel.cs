using Btx.Mobile.MockData;
using Btx.Mobile.Models;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Btx.Mobile.ViewModels
{
    public class ChatListViewModel : BaseViewModel
    {
        public ObservableRangeCollection<Chat> Chats { get; set; } = new ObservableRangeCollection<Chat>();

        public ChatListViewModel()
        {
            Chats.AddRange(ChatMockService.GetChats());
        }
    }
}
