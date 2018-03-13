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
        
        public MenuViewModel()
        {
            Items.Add(new BtxMenuItem("New Message", IconHelper.ACCOUNT, typeof(NewMessagePage)));
            Items.Add(new BtxMenuItem("New Group",IconHelper.ACCOUNTS, typeof(NewMessagePage)));
            Items.Add(new BtxMenuItem("Settings", IconHelper.SETTINGS, typeof(NewMessagePage)));
        }

        public async Task GoToPage(Type type)
        {
            var page = Activator.CreateInstance(type) as Page;
            
            await PushModalAsync(page);
            
        }
    }
}
