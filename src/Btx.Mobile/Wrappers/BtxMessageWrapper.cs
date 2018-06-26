using Btx.Client.Domain.Models;
using Btx.Mobile.Helpers;
using Btx.Mobile.Models;
using Btx.Mobile.Views.Modals;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Btx.Mobile.Wrappers
{
    public class BtxMessageWrapper : WrapperBase<BtxMessage>
    {
        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { _isBusy = value; OnPropertyChanged(); }
        }
        
        public BtxMessageStatus Status
        {
            get { return GetValue<BtxMessageStatus>(); }
            set { SetValue(value); }
        }

        public BtxMessageType BtxMessageType { get { return GetValue<BtxMessageType>(); } set { SetValue(value); } }
        
        public string Username
        {
            get { return Model?.Recipient?.Username; }
        }
        
        public Color ReadLabelColor
        {
            get
            {
                if (Status == BtxMessageStatus.Read)
                    return Color.Blue;
                else
                    return Color.Black;
            }
        }
        
        public string Body { get { return GetValue<string>(); } set { SetValue(value); } }

        public bool IsReadByUser { get { return GetValue<bool>(); } set { SetValue(value); } }

        public DateTimeOffset Date { get { return GetValue<DateTimeOffset>(); } set { SetValue(value); } }

        public string StatusIconFont
        {
            get
            {
                switch (Model.Status)
                {
                    case BtxMessageStatus.Pending:
                        return IconHelper.TIME;
                    case BtxMessageStatus.ServerDeliverd:
                        return IconHelper.CHECK;
                    case BtxMessageStatus.UserDeliverd:
                        return IconHelper.CHECK_ALL;
                    case BtxMessageStatus.Read:
                        return IconHelper.CHECK_ALL;
                    default:
                        return IconHelper.TIME;

                }
            }
        }

        #region ImageProps

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

        public ImageSource Source
        {
            get
            {
                return null;
            }
        }
        
        #endregion


        public BtxMessageWrapper(BtxMessage model) : base(model)
        {
            UploadCommand = new Command(async () => await Upload());
            TapCommand = new Command(async () => await OnTabbed());

            ShowRetryButton = false;
            ImageOpacity = 1;
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

           // await PopupNavigation.Instance.PushAsync(new ImageModalPage(LocalFilePath, ImageBytes), true);
        }
    }
}
