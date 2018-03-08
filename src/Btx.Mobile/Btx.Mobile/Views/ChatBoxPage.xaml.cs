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

        public ChatBoxPage(Chat chat)
        {
            InitializeComponent();
            chatTxtBox.ScrollView = textScroll;
            this.BindingContext = new ChatBoxViewModel(chat);

            ViewModel.OnChatItemAdded += OnChatItemAdded;

            //lvChatItems.Scrolled += LvChatItems_Scrolled;
            lvChatItems.ItemAppearing += OnListViewItemAppearing;
            lvChatItems.ItemDisappearing += OnListViewItemDisappearing;

            ScrollToEnd();

            App.ChatBoxPage = this;

        }

        private void OnListViewItemDisappearing(object sender, ItemVisibilityEventArgs e)
        {
            var currentItem = e.Item as ChatItem;

            Debug.WriteLine($"Item Disappearing: {currentItem.From}");

            int index = ViewModel.Items.IndexOf(currentItem);

            if (index == ViewModel.Items.Count - 1)
                IsAllowToScroll = false;
        }

        private void OnListViewItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            var newItem = e.Item as ChatItem;

            Debug.WriteLine($"Item Appearing: {newItem.From}");

            int index = ViewModel.Items.IndexOf(newItem);

            if (index == ViewModel.Items.Count - 1)
                IsAllowToScroll = true;
        }

        private void LvChatItems_Scrolled(object sender, ScrolledEventArgs e)
        {
            CurrentScrollPosition = e.ScrollY;

        }

        private void ScrollToEnd()
        {
            if (ViewModel.Items != null && ViewModel.Items.Count > 0)
                lvChatItems.ScrollTo(ViewModel.Items.Last(), ScrollToPosition.Center, false);
        }

        public void OnChatItemAdded(ChatItem item)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                if (ViewModel.Items.Count == 0)
                    return;

                if (IsAllowToScroll)
                    lvChatItems.ScrollTo(item, ScrollToPosition.MakeVisible, false);

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