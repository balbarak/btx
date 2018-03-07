using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Btx.Mobile.Models
{
    public class Chat : ObservableObject
    {
        private ChatItem lastChatItem = null;

        private string id;

        public string Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged(); }
        }
        
        private string title;

        public string Title
        {
            get { return title; }
            set { title = value; OnPropertyChanged(); }
        }
        
        public ObservableRangeCollection<ChatItem> Items { get; set; } = new ObservableRangeCollection<ChatItem>();

        public string LastMessage
        {
            get
            {
                if (lastChatItem != null)
                    return lastChatItem.Body;
                else
                    return "";
            }
        }

        public string LastMessageTime
        {
            get
            {
                if (lastChatItem == null)
                    return "";

                return lastChatItem.Date.ToString("hh:mm tt");
            }
        }

        public bool HasUnreadMessage
        {
            get
            {
                return Items.ToList().Any(a => a.IsRead == false);
            }
        }

        public int UnreadMessageCount
        {
            get
            {
                return Items.ToList().Count(a => a.IsRead == false);
            }
        }

        public Chat()
        {
            Items.CollectionChanged += Items_CollectionChanged;
        }

        public Chat(User user) : base()
        {
            this.Id = user.Id;
            this.Title = user.Nickname;
        }

        private void Items_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                lastChatItem = Items[Items.Count - 1];

                OnPropertyChanged(nameof(LastMessage));
                OnPropertyChanged(nameof(LastMessageTime));
                OnPropertyChanged(nameof(HasUnreadMessage));
                OnPropertyChanged(nameof(UnreadMessageCount));

            }

            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Replace)
            {
                OnPropertyChanged(nameof(HasUnreadMessage));
                OnPropertyChanged(nameof(UnreadMessageCount));
            }
        }
    }
}
