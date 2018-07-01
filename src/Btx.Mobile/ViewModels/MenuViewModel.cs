using Btx.Mobile.Helpers;
using Btx.Mobile.Models;
using Btx.Mobile.Services;
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
            Items.Add(new BtxMenuItem("Logout", IconHelper.LOGOFF, typeof(LogoutPage)));
        }

        public async Task GoToPage(Type type)
        {
            if (type == typeof(LogoutPage))
            {
                var result = await Application.Current.MainPage.DisplayAlert("Logout", "Are you sure ?", "Yes", "No");

                if (result)
                {
                    await BtxProtocolService.Instance.Client.Disconnect();

                    var chatListViewModel = ServiceLocator.Current.GetService<ChatListViewModel>();

                    chatListViewModel.Chats.Clear();

                    App.Instance.SetLoggedOutPage();
                }
            }
            else if (type == typeof(NewMessagePage))
            {
                App.MasterPage.IsPresented = false;

                await PushAsync(new NewMessagePage());
            }
            else
            {
                var page = Activator.CreateInstance(type) as Page;

                await PushModalAsync(page);
            }
            
            
        }
    }
}
