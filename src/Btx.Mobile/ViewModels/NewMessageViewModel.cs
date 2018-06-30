using Btx.Client.Domain.Models;
using Btx.Client.Domain.Search;
using Btx.Mobile.MockData;
using Btx.Mobile.Models;
using Btx.Mobile.Services;
using Btx.Mobile.Views;
using Btx.Mobile.Wrappers;
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
        public ObservableRangeCollection<BtxUserWrapper> Items { get; set; } = new ObservableRangeCollection<BtxUserWrapper>();

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
            var search = new BtxUserSearch()
            {
                Username = Keyword
            };

            var result = await BtxProtocolService.Instance.SearchBtxUser(search);

            foreach (var item in result.Result)
            {
                Items.Add(new BtxUserWrapper(item));
            }

        }

        public async Task GoToChatBox(Chat item)
        {
            App.MasterPage.IsPresented = false;

            //var chat = await App.ChatManager.AddChat(item);

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
