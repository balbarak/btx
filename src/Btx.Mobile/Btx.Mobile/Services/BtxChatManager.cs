using Btx.Mobile.Models;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Btx.Mobile.MockData;
using System.Threading;
using System.Diagnostics;
using Btx.Mobile.ViewModels;

namespace Btx.Mobile.Services
{
    public class BtxChatManager
    {
        public ObservableRangeCollection<ChatViewModel> ChatViewModels { get; set; } = new ObservableRangeCollection<ChatViewModel>();

        public ObservableRangeCollection<User> Users { get; set; } = new ObservableRangeCollection<User>();

        public BtxChatManager()
        {

            Users.AddRange(MockChatService.GetUsers());
            
            AddSampleChats();

            //MockChatService.StartSimulateChat(ChatViewModels.First().Id);

        }

        public async Task<ChatViewModel> AddChat(Chat chat)
        {
            var found = ChatViewModels.Where(a => a.Id == chat.Id).FirstOrDefault();

            if (found == null)
            {
                found = new ChatViewModel(chat);

                ChatViewModels.Insert(0,(found));
            }
            
            
            return found;
        }
        
        public ChatItem AddChatItem(string chatId,ChatItem item)
        {
            var chat = ChatViewModels.Where(a => a.Id == chatId).FirstOrDefault();
            
            chat.Items.Add(new ChatItemViewModel(item));

            SortChats();
            
            return item;
        }

        public ChatItemViewModel AddChatItem(string chatId, ChatItemViewModel item)
        {
            var chat = ChatViewModels.Where(a => a.Id == chatId).FirstOrDefault();

            chat.Items.Add(item);

            SortChats();

            return item;
        }

        private async Task SortChats()
        {
            var sortedItems = ChatViewModels.OrderByDescending(a => a.LastChatItem.Date).ToList();
            
            foreach (var item in sortedItems)
            {
                var newIndex = sortedItems.IndexOf(item);
                var oldIndex = ChatViewModels.IndexOf(item);

                ChatViewModels.Move(oldIndex, newIndex);
            }
            
        }

        private async Task AddSampleChats()
        {

            var chats = MockChatService.GetChats(50);

            foreach (var item in chats)
            {
                ChatViewModels.Add(new ChatViewModel(item));
            }


        }
    }
}
