using Btx.Mobile.Models;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Btx.Mobile.MockData;
using System.Threading;

namespace Btx.Mobile.Services
{
    public class BtxChatManager
    {
        public ObservableRangeCollection<Chat> Chats { get; set; } = new ObservableRangeCollection<Chat>();

        public ObservableRangeCollection<User> Users { get; set; } = new ObservableRangeCollection<User>();

        public BtxChatManager()
        {
            var currentThread = Thread.CurrentThread;

            Users.AddRange(MockChatService.GetUsers());
            Chats.AddRange(MockChatService.GetChats(2).OrderByDescending(a=> a.LastChatItem.Date).ToList());

            
            //MockChatService.StartSimulateChat(Chats.First().Id);

        }

        public async Task<Chat> AddChat(Chat chat)
        {
            var found = Chats.Where(a => a.Id == chat.Id).FirstOrDefault();

            if (found == null)
            {
                found = chat;
                Chats.Add(found);
            }
            
            return found;
        }
        
        public ChatItem AddChatItem(string chatId,ChatItem item)
        {
            var chat = Chats.Where(a => a.Id == chatId).FirstOrDefault();
            
            chat.Items.Add(item);
            
            var sortedItems = Chats.OrderByDescending(a => a.LastChatItem.Date).ToList();

            Chats.Clear();

            Chats.AddRange(sortedItems);
            
            if (App.ChatBoxPage?.ViewModel.Chat.Id == chatId)
            {
                App.ChatBoxPage.ViewModel.InvokeOnChatItemAdded(item);
            }
            
            return item;
        }
    }
}
