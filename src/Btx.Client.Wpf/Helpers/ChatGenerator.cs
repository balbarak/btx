using Btx.Client.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Btx.Client.Wpf.Helpers
{
    public class ChatGenerator
    {
        public static List<BtxMessage> GetRandomMessagesToSend(string userId,int count = 10)
        {
            List<BtxMessage> result = new List<BtxMessage>();
            
            for (int i = 0; i < count; i++)
            {
                var chatItem = new BtxMessage()
                {
                    RecipientId = userId,
                    Body = LoremGenerator.GenerateText(LoremGenerator.Random.Next(2, 30), LoremGenerator.Random.Next(1, 4)),
                };

                var percent = LoremGenerator.Random.NextDouble();
                
                result.Add(chatItem);
            }



            return result;
        }
    }
}
