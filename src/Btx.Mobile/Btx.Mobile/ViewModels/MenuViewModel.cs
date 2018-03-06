using Btx.Mobile.Helpers;
using Btx.Mobile.Models;
using Btx.Mobile.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Btx.Mobile.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        public ObservableCollection<BtxMenuItem> Items { get; set; } = new ObservableCollection<BtxMenuItem>();

        public ICommand GoToPageCommand { get; }

        public MenuViewModel()
        {
            GoToPageCommand = new Command(async () => await GoToPage());

            Items.Add(new BtxMenuItem("New Message", IconHelper.ACCOUNT, nameof(AboutPage)));
            Items.Add(new BtxMenuItem("New Group",IconHelper.ACCOUNTS,nameof(AboutPage)));
            Items.Add(new BtxMenuItem("Settings", IconHelper.SETTINGS, nameof(AboutPage)));

        }

        private async Task GoToPage()
        {

        }
    }
}
