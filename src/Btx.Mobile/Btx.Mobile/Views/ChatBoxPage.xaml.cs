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
        protected ChatBoxViewModel ViewModel => BindingContext as ChatBoxViewModel;

        public ChatBoxPage(Chat chat)
        {
            InitializeComponent();
            chatTxtBox.ScrollView = textScroll;
            this.BindingContext = new ChatBoxViewModel(chat);

            ViewModel.OnChatItemAdded += ViewModel_OnChatItemAdded;

            if (ViewModel.Items != null && ViewModel.Items.Count > 0)
                lvChatItems.ScrollTo(ViewModel.Items.Last(), ScrollToPosition.Center, false);

            

        }

        private void ViewModel_OnChatItemAdded(ChatItem item)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                if (ViewModel.Items.Count == 0)
                    return;

                lvChatItems.ScrollTo(item, ScrollToPosition.MakeVisible, true);

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