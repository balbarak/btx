using Btx.Mobile.Helpers;
using Btx.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Btx.Mobile.ViewModels
{
    public class ChatItemViewModel : BaseViewModel
    {
        private string id;
        public string Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged(); }
        }

        private ChatItemType itemType;
        public ChatItemType ItemType
        {
            get { return itemType; }
            set { itemType = value; OnPropertyChanged(); }
        }

        private ChatItemStatus status = ChatItemStatus.Pending;
        public ChatItemStatus Status
        {
            get { return status; }
            set
            {
                status = value;

                switch (value)
                {
                    case ChatItemStatus.Pending:
                        StatusIconFont = IconHelper.TIME;
                        break;
                    case ChatItemStatus.ServerDeliverd:
                        StatusIconFont = IconHelper.CHECK;
                        break;
                    case ChatItemStatus.UserDeliverd:
                        StatusIconFont = IconHelper.CHECK_ALL;
                        break;
                    case ChatItemStatus.Read:
                        StatusIconFont = IconHelper.CHECK_ALL;
                        break;
                    default:
                        break;
                }

                OnPropertyChanged(nameof(LabelColor));
                OnPropertyChanged(nameof(StatusIconFont));
                OnPropertyChanged();

            }
        }

        private string from;
        public string From
        {
            get { return from; }
            set { from = value; OnPropertyChanged(); }
        }

        private string body;
        public string Body
        {
            get { return body; }
            set { body = value; OnPropertyChanged(); }
        }
        
        private DateTimeOffset date;
        public DateTimeOffset Date
        {
            get { return date; }
            set { date = value; OnPropertyChanged(); }
        }

        private string statusIconFont = IconHelper.TIME;
        public string StatusIconFont
        {
            get { return statusIconFont; }
            set { statusIconFont = value; OnPropertyChanged(); }
        }

        private bool isRead;
        public bool IsRead
        {
            get { return isRead; }
            set { isRead = value; OnPropertyChanged(); }
        }

        private string localFilePath;
        public string LocalFilePath
        {
            get { return localFilePath; }
            set { localFilePath = value; OnPropertyChanged(); }
        }
        
        public Color LabelColor
        {
            get
            {
                if (Status == ChatItemStatus.Read)
                    return Color.Blue;
                else
                    return Color.Black;
            }
        }


        public ICommand UploadCommand { get; }

        public ChatItemViewModel()
        {
            Date = DateTime.Now;
            UploadCommand = new Command(Upload);
        }

        public ChatItemViewModel(ChatItem entity) : this()
        {
            this.Id = entity.Id;
            this.Date = entity.Date;
            this.Body = entity.Body;
            this.From = entity.From;
            this.ItemType = entity.ItemType;
            this.Status = entity.Status;
        }

        private void Upload()
        {

        }
    }
}
