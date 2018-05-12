using Btx.Mobile.MockData;
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

        public ICommand CloseCommand { get; }

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
            CloseCommand = new Command(Close);
            Title = "New Message";
        }

        private async Task Search()
        {
            if (String.IsNullOrWhiteSpace(Keyword))
            {
                Items = App.ChatManager.Users;

                return;
            }

            var result = App.ChatManager.Users.Where(a => a.Nickname.Contains(keyword));

            Items = new ObservableRangeCollection<User>(result);
        }

        public async Task GoToChatBox(Chat item)
        {
            App.MasterPage.IsPresented = false;

            var chat = await App.ChatManager.AddChat(item);

            //PushAsync(new ChatBoxPage());

            await PopModalAsync();

            

        }

        public void Close()
        {
            App.MasterPage.IsPresented = false;

            PopModalAsync();
        }
    }
}
