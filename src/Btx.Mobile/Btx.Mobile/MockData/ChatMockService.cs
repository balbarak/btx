using Btx.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Btx.Mobile.MockData
{
    public class ChatMockService
    {
        public static List<ChatItem> GetChatItem()
        {
            List<ChatItem> result = new List<ChatItem>();

            int max = LoremGenerator.Random.Next(2, 100);

            for (int i = 0; i < max; i++)
            {
                result.Add(new ChatItem()
                {
                    From = LoremGenerator.GenerateText(1),
                    Body = LoremGenerator.GenerateText(LoremGenerator.Random.Next(2,30), LoremGenerator.Random.Next(1, 4)),
                    Date = DateTime.Now.AddMinutes(LoremGenerator.Random.Next(10)),
                    ItemType = ChatItem.ChatItemType.Incoming,
                    Status = ChatItem.ChatItemStatus.Read
                });
            }

            return result;
        }

        public static List<Chat> GetChats()
        {
            List<Chat> result = new List<Chat>();

            for (int i = 0; i < 30; i++)
            {
                var chat = new Chat()
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = LoremGenerator.GenerateText(3,1)
                };

                chat.Items.AddRange(GetChatItem());

                result.Add(chat);
            }

            return result;
            
        }
    }
}
