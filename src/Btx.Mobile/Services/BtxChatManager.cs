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
using Btx.Client.Application.Services;
using Btx.Client.Domain.Models;
using Btx.Mobile.Wrappers;
using Btx.Client;

namespace Btx.Mobile.Services
{
    public class BtxChatManager
    {
        public BtxClient Client { get; set; } = new BtxClient();

        public ObservableRangeCollection<User> Users { get; set; } = new ObservableRangeCollection<User>();

        public ObservableRangeCollection<BtxThreadWrapper> BtxThreads { get; private set; } = new ObservableRangeCollection<BtxThreadWrapper>();

        public BtxThreadWrapper CurrentThread { get; set; }
        
        public BtxChatManager()
        {
            //AddRandomThreads();

            //Users.AddRange(MockChatService.GetUsers());
            
            //AddSampleChats();

            //MockChatService.StartSimulateChat(ChatViewModels.First().Id);

        }
        
        private async Task SortChats()
        {
            var sortedItems = BtxThreads.OrderByDescending(a => a.LastMessageDate).ToList();
            
            foreach (var item in sortedItems)
            {
                var newIndex = sortedItems.IndexOf(item);
                var oldIndex = BtxThreads.IndexOf(item);

                BtxThreads.Move(oldIndex, newIndex);
            }
            
        }
        
        private void AddRandomThreads()
        {
            var threads = MockChatService.GetRandomThreads(7);

            foreach (var item in threads)
            {
                item.Messages = MockChatService.GetRandomMessages();
               
                BtxThreadService.Instance.AddOrUpdate(item);
            }
        }
    }
}
