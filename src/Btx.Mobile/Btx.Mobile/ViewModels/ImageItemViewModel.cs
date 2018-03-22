using Btx.Mobile.Models;
using Btx.Mobile.Views.Modals;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Btx.Mobile.ViewModels
{
    public class ImageItemViewModel : ChatItemViewModel
    {
        private bool showRetryButton = true;

        public bool ShowRetryButton
        {
            get { return showRetryButton; }
            set
            {
                showRetryButton = value;
                OnPropertyChanged();
            }
        }

        private double imageOpacity = 0.3;

        public double ImageOpacity
        {
            get { return imageOpacity; }
            set { imageOpacity = value; OnPropertyChanged(); }
        }

        private string localFilePath;
        public string LocalFilePath
        {
            get { return localFilePath; }
            set { localFilePath = value; OnPropertyChanged(); }
        }


        public ICommand UploadCommand { get; }

        public ICommand TapCommand { get; }

        public ImageItemViewModel() : base()
        {
            UploadCommand = new Command(async () => await Upload());
            TapCommand = new Command(async () => await OnTabbed());
        }

        public ImageItemViewModel(ChatItemType itemType,string filePath) : this()
        {
            ItemType = itemType;
            LocalFilePath = filePath;

            if (itemType == ChatItemType.OutgoingFile)
                Upload();
        }

        private async Task Upload()
        {
            IsBusy = true;
            ShowRetryButton = false;
            ImageOpacity = 0.3;

            await Task.Delay(3000);

            ShowRetryButton = false;
            ImageOpacity = 1;
            IsBusy = false;
        }

        private async Task OnTabbed()
        {
            if (IsBusy || ShowRetryButton)
                return;

            await PopupNavigation.Instance.PushAsync(new ImageModalPage(LocalFilePath), true);
        }
    }
}
