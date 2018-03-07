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
	public partial class ChatListPage : MasterDetailPage
	{
        protected ChatListViewModel ViewModel => BindingContext as ChatListViewModel;

        public ChatListPage ()
		{
			InitializeComponent ();
           
            this.BindingContext = new ChatListViewModel();

            App.ChatListPage = this;
   
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
            await ViewModel.GoToChatBox(e.Item as Chat);
        }
    }
}