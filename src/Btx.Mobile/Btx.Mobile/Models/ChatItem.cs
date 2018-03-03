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

        private ChatItemType itemType;
        public ChatItemType ItemType
        {
            get { return itemType; }
            set { itemType = value; OnPropertyChanged(); }
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
