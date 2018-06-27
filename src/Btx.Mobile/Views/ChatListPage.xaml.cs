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
        public ChatListViewModel ViewModel { get; } = ServiceLocator.Current.GetService<ChatListViewModel>();

        public ChatListPage()
        {
            InitializeComponent();

            this.BindingContext = ViewModel;
            
        }
        
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await ViewModel.OnAppearing();
            
        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();

            await ViewModel.OnDisappearing();
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