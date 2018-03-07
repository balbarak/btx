using Btx.Mobile.MockData;
using Btx.Mobile.Models;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Btx.Mobile.ViewModels
{
    public class NewMessageViewModel : BaseViewModel
    {
        public ObservableRangeCollection<User> Items { get; set; } = new ObservableRangeCollection<User>();

        public NewMessageViewModel()
        {

            Items.AddRange(ChatMockService.GetUsers());

            Title = "New Message";
        }
    }
}
