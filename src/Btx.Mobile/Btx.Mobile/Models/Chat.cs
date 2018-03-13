using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Btx.Mobile.Models
{
    public class Chat
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public List<ChatItem> Items { get; set; }

        public Chat()
        {
            Items = new List<ChatItem>();
        }

        public Chat(User user) : this()
        {
            this.Id = user.Id;
            this.Title = user.Nickname;
        }
        
    }
}
