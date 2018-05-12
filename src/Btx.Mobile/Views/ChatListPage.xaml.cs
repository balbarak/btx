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
	public partial class ChatListPage : ContentPage
	{
        public ChatListViewModel ViewModel => BindingContext as ChatListViewModel;

        public ChatListPage ()
		{
			InitializeComponent ();
           
            this.BindingContext = new ChatListViewModel();

            App.ChatListPage = this;
            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private void OnSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
            }

            ((ListView)sender).SelectedItem = null; //uncomment line if you want to disable the visual selection state.
            
        }

        private void OnTabbed(object sender, ItemTappedEventArgs e)
        {
            if (ViewModel.IsBusy)
                return;

            Device.BeginInvokeOnMainThread(async () =>
            {
               await ViewModel.GoToChatBox();

            });
            
        }
    }
}