﻿using Btx.Mobile.MockData;
using Btx.Mobile.Models;
using Btx.Mobile.Views;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Btx.Mobile.ViewModels
{
    public class NewMessageViewModel : BaseViewModel
    {
        public ObservableRangeCollection<User> Items { get; set; } = App.ChatManager.Users;

        private string keyword;

        public string Keyword
        {
            get { return keyword; }
            set
            {
                keyword = value;
                OnPropertyChanged();
                SearchCommand.Execute(null);
                OnPropertyChanged(nameof(Items));
            }
        }
        
        public ICommand SearchCommand { get; }
        
        public NewMessageViewModel()
        {
            SearchCommand = new Command(async () => await Search());

            Title = "New Message";
        }

        private async Task Search()
        {
            var result = App.ChatManager.Users.Where(a => a.Nickname.Contains(keyword));

            Items = new ObservableRangeCollection<User>(result);
        }

        public async Task GoToChatBox(Chat item)
        {
            var chat = await App.ChatManager.AddChat(item);

            await PushAsync(new ChatBoxPage(chat));
           
            await PopModalAsync();

        }
    }
}