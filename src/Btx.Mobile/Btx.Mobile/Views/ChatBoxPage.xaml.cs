using Btx.Mobile.Models;
using Btx.Mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Btx.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatBoxPage : ContentPage
    {
        public ChatBoxViewModel ViewModel => BindingContext as ChatBoxViewModel;

        public double CurrentScrollPosition { get; set; }

        public bool IsAllowToScroll { get; set; }

        public ChatBoxPage()
        {
            //this.chatTxtBox.Focus();
        }

        public ChatBoxPage(ChatViewModel chat) : this()
        {
            InitializeComponent();
            chatTxtBox.ScrollView = textScroll;
            this.BindingContext = new ChatBoxViewModel(chat);

            SetEvents();

            ScrollToEnd();

        }

        ~ChatBoxPage()
        {
            RemoveEvents();
        }

        private void SetEvents()
        {
            lvChatItems.ItemAppearing += OnListViewItemAppearing;
            lvChatItems.ItemDisappearing += OnListViewItemDisappearing;
            ViewModel.Chat.OnChatItemAdded += OnChatItemAdded;

        }

        private void RemoveEvents()
        {
            lvChatItems.ItemAppearing -= OnListViewItemAppearing;
            lvChatItems.ItemDisappearing -= OnListViewItemDisappearing;
            ViewModel.Chat.OnChatItemAdded -= OnChatItemAdded;
        }

        protected override void OnAppearing()
        {
            ScrollToEnd();

            base.OnAppearing();
        }

        private void OnListViewItemDisappearing(object sender, ItemVisibilityEventArgs e)
        {
            var currentItem = e.Item as ChatItemViewModel;

            int index = ViewModel.Items.IndexOf(currentItem);

            if (index == ViewModel.Items.Count - 1)
                IsAllowToScroll = false;
        }

        private void OnListViewItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            var newItem = e.Item as ChatItemViewModel;

            int index = ViewModel.Items.IndexOf(newItem);

            if (index == ViewModel.Items.Count - 1)
                IsAllowToScroll = true;
        }

        private void ScrollToEnd()
        {
            
            if (ViewModel.Items != null && ViewModel.Items.Count > 0)
                lvChatItems.ScrollTo(ViewModel.Items.Last(), ScrollToPosition.End, false);
        }


        private void OnChatItemAdded(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                if (IsAllowToScroll)
                    ScrollToEnd();

            });
        }

        private void OnSelection(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
            }

            ((ListView)sender).SelectedItem = null; //uncomment line if you want to disable the visual selection state.
        }

    }
}