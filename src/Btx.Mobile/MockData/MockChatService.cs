using Btx.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Threading;
using System.Reflection;
using System.IO;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using Btx.Client.Domain.Models;

namespace Btx.Mobile.MockData
{
    public class MockChatService
    {
        public static List<ChatItem> GetChatItem()
        {
            List<ChatItem> result = new List<ChatItem>();

            int max = LoremGenerator.Random.Next(2, 1200);

            for (int i = 0; i < max; i++)
            {
                var chatItem = new ChatItem()
                {
                    From = LoremGenerator.GenerateText(1),
                    Body = LoremGenerator.GenerateText(LoremGenerator.Random.Next(2, 30), LoremGenerator.Random.Next(1, 4)),
                    Date = DateTime.Now.AddMinutes(LoremGenerator.Random.Next(-50, -3)),
                    ItemType = ChatItemType.Incoming,
                    Status = ChatItemStatus.Read
                };

                var percent = LoremGenerator.Random.NextDouble();

                if (percent > 0.7)
                    chatItem.ItemType = ChatItemType.Outgoing;

                if (percent > 0.4)
                {
                    chatItem.ItemType = ChatItemType.IncomingImage;
                    chatItem.ImageBytes = GetRandomImage();
                }


                result.Add(chatItem);
            }

            

            return result;
        }

        public static List<BtxMessage> GetRandomMessages()
        {
            List<BtxMessage> result = new List<BtxMessage>();

            int max = LoremGenerator.Random.Next(2, 1200);

            for (int i = 0; i < max; i++)
            {
                var chatItem = new BtxMessage()
                {
                    RecipientId = LoremGenerator.GenerateText(1),
                    Body = LoremGenerator.GenerateText(LoremGenerator.Random.Next(2, 30), LoremGenerator.Random.Next(1, 4)),
                    Date = DateTime.Now.AddMinutes(LoremGenerator.Random.Next(-50, -3)),
                    BtxMessageType = BtxMessageType.Incoming,
                    Status = BtxMessageStatus.Read
                };

                var percent = LoremGenerator.Random.NextDouble();

                if (percent > 0.7)
                    chatItem.BtxMessageType = BtxMessageType.Outgoing;

                if (percent > 0.4)
                {
                    chatItem.BtxMessageType = BtxMessageType.IncomingImage;
                   
                }


                result.Add(chatItem);
            }



            return result;
        }


        public static byte[] GetRandomImage()
        {
            byte[] result;

            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(MockData.LoremGenerator)).Assembly;
            Stream stream = assembly.GetManifestResourceStream("Btx.Mobile.MockData.samples.json");
            using (StreamReader sr = new StreamReader(stream))
            {
                var json = sr.ReadToEnd();

                var data = JsonConvert.DeserializeObject<List<ImageModel>>(json);

                int random = LoremGenerator.Random.Next(0, 2);

                result = FromBase64(data[random].Data, out string type);

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

        public static List<BtxThread> GetRandomThreads(int max)
        {
            List<BtxThread> result = new List<BtxThread>();

            for (int i = 0; i < max; i++)
            {
                var chat = new BtxThread()
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = LoremGenerator.GenerateText(3, 1),

                };

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
        
        public static byte[] FromBase64(string data, out string type)
        {
            byte[] result = null;
            try
            {
                var base64Data = Regex.Match(data, @"data:image/(?<type>.+?),(?<data>.+)").Groups["data"].Value;
                type = Regex.Match(data, @"data:image/(?<type>.+?),(?<data>.+)").Groups["type"].Value.Replace(";base64", "");
                result = Convert.FromBase64String(base64Data);

                type = type.Insert(0, "image/");
            }
            catch (Exception)
            {
                type = "";
            }

            return result;
        }

    }
}
