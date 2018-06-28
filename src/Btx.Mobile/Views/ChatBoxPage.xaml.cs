using Btx.Client.Domain.Models;
using Btx.Mobile.ControlEventArgs;
using Btx.Mobile.Models;
using Btx.Mobile.ViewModels;
using Btx.Mobile.Wrappers;
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
        private int _lastItemVisibleIndex = 0;

        public ChatBoxViewModel ViewModel { get; } = ServiceLocator.Current.GetService<ChatBoxViewModel>();

        public double CurrentScrollPosition { get; set; }

        public bool IsAllowToScroll { get; set; } = true;

        public ChatBoxPage()
        {
            InitializeComponent();

            this.BindingContext = ViewModel;

            lvChatItems.OnScroll += OnScroll;
            lvChatItems.ItemAppearing += OnItemAppearing;
            lvChatItems.ItemDisappearing += OnItemDisappearing;

            chatTxtBox.ScrollView = textScroll;

            ViewModel.Items.CollectionChanged += Items_CollectionChanged;

            ScrollToEnd();
        }

        private async void OnScroll(object sender, EventArgs e)
        {

            var args = e as ChatBoxListEventArgs;

            if (args.FirstItemIndex + args.VisibleItemCount >= args.TotalItemsCount - 1)
                IsAllowToScroll = true;
            else
                IsAllowToScroll = false;


            //scrolling up
            if (args.FirstItemIndex < _lastItemVisibleIndex)
            {
                Debug.WriteLine("Scrolling Up");
                Debug.WriteLine("First item index" + args.FirstItemIndex);

                if (args.FirstItemIndex == 0)
                {
                    var item = ViewModel.Items[0];

                    await ViewModel.LoadMessages(true);

                    lvChatItems.ScrollTo(item, ScrollToPosition.Center, false);
                }

            }
            else if (args.FirstItemIndex > _lastItemVisibleIndex)
            {
                Debug.WriteLine("Scrolling down");
                //await ViewModel.LoadMessages(true);
            }

            _lastItemVisibleIndex = args.FirstItemIndex;
        }

        private void OnItemDisappearing(object sender, ItemVisibilityEventArgs e)
        {
            var index = ViewModel.Items.IndexOf((BtxMessageWrapper)e.Item);
            var count = ViewModel.Items.Count - 1;
        }

        private async void OnItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            var index = ViewModel.Items.IndexOf((BtxMessageWrapper)e.Item);
            var count = ViewModel.Items.Count - 1;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            await ViewModel.OnAppearing();
        }

        protected async override void OnDisappearing()
        {
            base.OnDisappearing();

            await ViewModel.OnDisappearing();
        }

        private void Items_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                if (IsAllowToScroll)
                {
                    ScrollToEnd();
                }
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