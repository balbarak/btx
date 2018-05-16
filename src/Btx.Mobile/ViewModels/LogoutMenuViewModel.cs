using Btx.Mobile.Models;
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

        public async Task GoToPage(Type type)
        {
            var page = Activator.CreateInstance(type) as Page;

            await PushModalAsync(page);

        }
    }

}
