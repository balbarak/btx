using Btx.Mobile.Helpers;
using Btx.Mobile.Models;
using Btx.Mobile.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Btx.Mobile.ViewModels
{
    public class LogoutMenuViewModel : BaseViewModel
    {
        public ObservableCollection<BtxMenuItem> Items { get; set; } = new ObservableCollection<BtxMenuItem>();

        public LogoutMenuViewModel()
        {
            Items.Add(new BtxMenuItem("Register", IconHelper.ACCOUNT, typeof(RegisterPage)));
            Items.Add(new BtxMenuItem("Login", IconHelper.ACCOUNT, typeof(LoginPage)));
        }

        public async Task GoToPage(Type type)
        {
            var page = Activator.CreateInstance(type) as Page;

            App.MasterPage.IsPresented = false;

            await PushAsync(page);

        }
    }

}
