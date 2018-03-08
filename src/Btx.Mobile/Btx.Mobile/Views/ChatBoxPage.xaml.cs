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

        public ChatBoxPage(Chat chat)
        {
            InitializeComponent();
            chatTxtBox.ScrollView = textScroll;
            this.BindingContext = new ChatBoxViewModel(chat);

            ViewModel.OnChatItemAdded += ViewModel_OnChatItemAdded;

            lvChatItems.Scrolled += LvChatItems_Scrolled;
            ScrollToEnd();

            App.ChatBoxPage = this;

        }

        private void LvChatItems_Scrolled(object sender, ScrolledEventArgs e)
        {
            Debug.WriteLine($"Current Scroll: {e.ScrollY}");

            CurrentScrollPosition = e.ScrollY;
        }

        private void ScrollToEnd()
        {
            if (ViewModel.Items != null && ViewModel.Items.Count > 0)
                lvChatItems.ScrollTo(ViewModel.Items.Last(), ScrollToPosition.Center, false);
        }

        public void ViewModel_OnChatItemAdded(ChatItem item)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                if (ViewModel.Items.Count == 0)
                    return;

                if (CurrentScrollPosition == 0)
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