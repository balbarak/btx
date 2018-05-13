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

        public ChatListPage()
        {
            InitializeComponent();

            this.BindingContext = new ChatListViewModel();

            App.ChatListPage = this;

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private async void OnTabbed(object sender, ItemTappedEventArgs e)
        {
            if (!this.IsEnabled)
                return;

            this.IsEnabled = false;

            await ViewModel.GoToChatBox();

            this.IsEnabled = true;


        }
    }
}