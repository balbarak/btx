using Btx.Mobile.Models;
using Btx.Mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Btx.Mobile.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LogoutMenuPage : ContentPage
	{
        protected LogoutMenuViewModel ViewModel => BindingContext as LogoutMenuViewModel;


        public LogoutMenuPage ()
		{
			InitializeComponent ();
            this.BindingContext = new LogoutMenuViewModel();
        }

        private void OnSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
            }

           ((ListView)sender).SelectedItem = null; //uncomment line if you want to disable the visual selection state.

        }

        private async Task OnTabbed(object sender, ItemTappedEventArgs e)
        {
            var item = e.Item as BtxMenuItem;

            //App.ChatListPage.IsPresented = false;

            await ViewModel.GoToPage(item.MenuType);
        }
    }
}