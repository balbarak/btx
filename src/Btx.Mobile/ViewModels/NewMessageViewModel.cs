using Btx.Client.Application.Services;
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
        private int _pageNumber = 1;

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

            LoadUsers();
        }

        private async Task Search()
        {
            if (string.IsNullOrWhiteSpace(Keyword))
                return;

            var search = new BtxUserSearch()
            {
                Username = Keyword
            };

            var result = await BtxProtocolService.Instance.SearchBtxUser(search);

            foreach (var item in result.Result)
            {
                var found = Items.Where(a => a.Id == item.Id).FirstOrDefault();

                if (found == null)
                    Items.Add(new BtxUserWrapper(item));
            }

        }

        public async Task GoToChatBox(BtxUserWrapper item)
        {
            App.MasterPage.IsPresented = false;

            var thread = new BtxThread(item.Model);

            BtxProtocolService.Instance.CurrentThread = new BtxThreadWrapper(thread);

            await PushAsync(new ChatBoxPage(),true);

            await PopModalAsync();

            BtxProtocolService.Instance.AddThreadToChatList(thread);
        }

        public async Task LoadUsers()
        {
            IsBusy = true;

            var search = new SearchCriteria<BtxUser>()
            {
                PageNumber = _pageNumber,
            };

            var result = BtxUserService.Instance.Search(search);

            foreach (var item in result.Result)
            {
                Items.Add(new BtxUserWrapper(item));
            }

            if (result.Result.Any())
                _pageNumber++;

            await Task.CompletedTask;

            IsBusy = false;
        }

        public void Close()
        {
            App.MasterPage.IsPresented = false;

            PopModalAsync();
        }
    }
}
