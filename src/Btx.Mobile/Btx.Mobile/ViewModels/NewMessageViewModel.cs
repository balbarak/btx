using Btx.Mobile.MockData;
using Btx.Mobile.Models;
using Btx.Mobile.Views;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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

        public async Task GoToChatBox(Chat item)
        {
            
            await PushAsync(new ChatBoxPage(item));

            await PopModalAsync();

        }
    }
}
