using Btx.Client.Domain.Models;
using Btx.Mobile.Models;
using Btx.Mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
        public ChatBoxViewModel ViewModel { get; } = ServiceLocator.Current.GetService<ChatBoxViewModel>();

        public double CurrentScrollPosition { get; set; }

        public bool IsAllowToScroll { get; set; }

        public ChatBoxPage()
        {
            InitializeComponent();

            this.BindingContext = ViewModel;

            chatTxtBox.ScrollView = textScroll;
            
            ViewModel.Items.CollectionChanged += Items_CollectionChanged;

            ScrollToEnd();
        }

        private void Items_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                ScrollToEnd();
            }
        }

        private void ScrollToEnd()
        {
            if (ViewModel.Items != null && ViewModel.Items.Count > 0)
                lvChatItems.ScrollTo(ViewModel.Items.Last(), ScrollToPosition.End, false);
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