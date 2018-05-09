using Btx.Mobile.Models;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Btx.Mobile.ViewModels
{
    public class ChatViewModel : BaseViewModel
    {
        public event EventHandler OnChatItemAdded;

        private string id;

        public string Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged(); }
        }

        private string chatTitle;

        public string ChatTitle
        {
            get { return chatTitle; }
            set { chatTitle = value; OnPropertyChanged(); }
        }

        public string LastMessage
        {
            get
            {
                if (LastChatItem != null)
                    return LastChatItem.Body;
                else
                    return "";
            }
        }

        public string LastMessageTime
        {
            get
            {
                if (LastChatItem == null)
                    return "";

                return LastChatItem.Date.ToString("hh:mm tt");
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

        public ChatItemViewModel LastChatItem
        {
            get
            {
                if (Items.Count == 0)
                    return null;

                return Items[Items.Count - 1];

            }
        }
        
        public ObservableRangeCollection<ChatItemViewModel> Items { get; set; } = new ObservableRangeCollection<ChatItemViewModel>();

        public ChatViewModel()
        {
            Items.CollectionChanged += CollectionChanged;
        }
        
        public ChatViewModel(Chat entity) : this()
        {
            this.Title = entity.Title;
            this.Id = entity.Id;
            
            if (entity.Items != null && entity.Items.Count > 0)
            {
                foreach (var item in entity.Items)
                {
                    if (item.ItemType == ChatItemType.IncomingImage)
                        Items.Add(new ImageItemViewModel(item));
                    else
                        Items.Add(new ChatItemViewModel(item));
                }
            }
        }

        private void CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                OnPropertyChanged(nameof(LastMessage));
                OnPropertyChanged(nameof(LastMessageTime));
                OnPropertyChanged(nameof(HasUnreadMessage));
                OnPropertyChanged(nameof(UnreadMessageCount));

                OnChatItemAdded?.Invoke(this, EventArgs.Empty);

            }

            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Replace)
            {
                OnPropertyChanged(nameof(HasUnreadMessage));
                OnPropertyChanged(nameof(UnreadMessageCount));
            }
        }


    }
}
