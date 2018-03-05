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
        protected ChatListViewModel ViewModel => BindingContext as ChatListViewModel;

        public ChatListPage ()
		{
			InitializeComponent ();

            this.BindingContext = new ChatListViewModel();
        }
	}
}