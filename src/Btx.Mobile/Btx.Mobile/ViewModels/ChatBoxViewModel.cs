using Btx.Mobile.MockData;
using Btx.Mobile.Models;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Btx.Mobile.ViewModels
{
    public delegate void ChatItemAddedEventHandler(ChatItem item);

    public class ChatBoxViewModel : BaseViewModel
    {
        public event ChatItemAddedEventHandler OnChatItemAdded;

        public ObservableRangeCollection<ChatItem> Items { get; set; } = new ObservableRangeCollection<ChatItem>();

        private string messageToSend;

        public string MessageToSend
        {
            get { return messageToSend; }
            set { messageToSend = value; OnPropertyChanged(); }
        }

        public ICommand SendCommand { get; }
        
        public ChatBoxViewModel()
        {
            Items.AddRange(ChatItemMock.GetItems());

            SendCommand = new Command(async () => { await Send(); });
        }

        private async Task Send()
        {

            var chatMessage = new ChatItem(MessageToSend);
            chatMessage.Date = DateTimeOffset.Now;
            chatMessage.ItemType = ChatItem.ChatItemType.Outgoing;

            Items.Add(chatMessage);

            MessageToSend = "";

            OnChatItemAdded?.Invoke(chatMessage);
            
        }
    }
}
