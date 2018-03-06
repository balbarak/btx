using Btx.Mobile.Helpers;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Btx.Mobile.Models
{
    public class ChatItem : ObservableObject
    {
        public enum ChatItemType
        {
            Incoming = 0,
            Outgoing = 1,
            Info = 2
        }
        public enum ChatItemStatus
        {
            Pending = 0,
            ServerDeliverd = 1,
            UserDeliverd = 2,
            Read = 3
        };

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

                OnPropertyChanged(nameof(StatusIconFont));
                OnPropertyChanged();

            }
        }


        private string statusIconFont = IconHelper.TIME;
        public string StatusIconFont
        {
            get { return statusIconFont; }
            set { statusIconFont = value; OnPropertyChanged(); }
        }

        private string id;
        public string Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged(); }
        }

        private string from;
        public string From
        {
            get { return from; }
            set { from = value; }
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

        private bool isRead;

        public bool IsRead
        {
            get { return isRead; }
            set { isRead = value; OnPropertyChanged(); }
        }

        public ChatItem()
        {
            id = Guid.NewGuid().ToString();
        }

        public ChatItem(string msg) : base() 
        {
            this.Body = msg;
        }

    }
}
