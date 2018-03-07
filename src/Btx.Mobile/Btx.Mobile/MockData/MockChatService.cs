using Btx.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Threading;

namespace Btx.Mobile.MockData
{
    public class MockChatService
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
                    Body = LoremGenerator.GenerateText(LoremGenerator.Random.Next(2, 30), LoremGenerator.Random.Next(1, 4)),
                    Date = DateTime.Now.AddMinutes(LoremGenerator.Random.Next(-50, -3)),
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
                    Title = LoremGenerator.GenerateText(3, 1)
                };

                chat.Items.AddRange(GetChatItem());

                result.Add(chat);
            }

            return result;

        }

        public static List<Chat> GetChats(int max)
        {
            List<Chat> result = new List<Chat>();

            for (int i = 0; i < max; i++)
            {
                var chat = new Chat()
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = LoremGenerator.GenerateText(3, 1)
                };

                chat.Items.AddRange(GetChatItem());

                result.Add(chat);
            }

            return result;

        }

        public static List<User> GetUsers()
        {
            var count = 40;

            List<User> result = new List<User>();

            for (int i = 0; i < count; i++)
            {
                result.Add(new User()
                {
                    Username = LoremGenerator.GenerateText(1),
                    Id = Guid.NewGuid().ToString(),
                    LastSeen = DateTime.Now.AddSeconds(LoremGenerator.Random.Next(-309483, 0)),
                    Nickname = LoremGenerator.GenerateText(2),
                    RegisteredDate = DateTime.Now
                });
            }

            return result;
        }


        public static async Task StartSimulate()
        {
            for (int i = 0; i < 100; i++)
            {
                App.ChatListPage?.ViewModel?.ChangeTitle("Updating ...");

                await Task.Delay(LoremGenerator.Random.Next(10, 2000));

                var chats = GetChats(LoremGenerator.Random.Next(10, 100));

                var current = App.ChatManager.Chats.OrderByDescending(a => a.LastChatItem.Date).FirstOrDefault();

                var items = GetChatItem();

                foreach (var item in items)
                {
                    App.ChatManager.AddChatItem(current.Id, item);
                }



                foreach (var item in chats)
                {
                    App.ChatManager.AddChat(item);
                }

            }

            App.ChatListPage?.ViewModel?.ChangeTitle("BTX Chat");


        }
    }
}
