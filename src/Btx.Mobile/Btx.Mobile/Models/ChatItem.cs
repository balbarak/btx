using Btx.Mobile.Helpers;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Btx.Mobile.Models
{
    public class ChatItem 
    {
        public string Id { get; set; }
        
        public ChatItemType ItemType { get; set; }

        public ChatItemStatus Status { get; set; }
        
        public string From { get; set; }

        public string Body { get; set; }

        public DateTimeOffset Date { get; set; }
        
        public ChatItem()
        {
            Id = Guid.NewGuid().ToString();
            Date = DateTime.Now;
        }

        public ChatItem(string msg) : base() 
        {
            this.Body = msg;
        }

    }
}
