using Btx.Mobile.MockData;
using Btx.Mobile.Models;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Btx.Mobile.ViewModels
{
    public class ChatBoxViewModel : BaseViewModel
    {
        public ObservableRangeCollection<ChatItem> Items { get; set; } = new ObservableRangeCollection<ChatItem>();

        public ChatBoxViewModel()
        {
            Items.AddRange(ChatItemMock.GetItems());
        }
    }
}
