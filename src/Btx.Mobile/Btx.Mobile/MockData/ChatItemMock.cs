using Btx.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Btx.Mobile.MockData
{
    public class ChatItemMock
    {
        public static List<ChatItem> GetItems()
        {
            return new List<ChatItem>()
            {
                new ChatItem(){ From = "Bader",Body = "Hi There", ItemType = ChatItem.ChatItemType.Incoming,Date = DateTime.Now },
                new ChatItem(){ From = "Khalid",Body = "Hi", ItemType = ChatItem.ChatItemType.Incoming,Date = DateTime.Now.AddMinutes(2)},
                new ChatItem(){ From = "Fahad",Body = "Welcome to our chat", ItemType = ChatItem.ChatItemType.Incoming,Date = DateTime.Now.AddMinutes(5)},
                new ChatItem(){ From = "Fahad",Body = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has ", ItemType = ChatItem.ChatItemType.Incoming,Date = DateTime.Now.AddMinutes(5)},
                new ChatItem(){ From = "Fahad",Body = "Welcome and you are suck !", ItemType = ChatItem.ChatItemType.Outgoing,Date = DateTime.Now.AddMinutes(5)}

            };
        }
    }
}
