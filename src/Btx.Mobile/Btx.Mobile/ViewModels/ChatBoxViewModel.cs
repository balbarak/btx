using Btx.Mobile.MockData;
using Btx.Mobile.Models;
using Btx.Mobile.Views;
using MvvmHelpers;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
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

        public Chat Chat { get; set; }

        public ObservableRangeCollection<ChatItem> Items { get { return Chat.Items; } }
        
        private string messageToSend;

        public string MessageToSend
        {
            get { return messageToSend; }
            set { messageToSend = value; OnPropertyChanged(); }
        }

        public ICommand SendCommand { get; }

        public ICommand SelectAttachmentCommand { get; }

        public ChatBoxViewModel(Chat chat)
        {
            this.Chat = chat;
            this.Title = chat.Title;

            SendCommand = new Command(async () => { await Send(); });
            SelectAttachmentCommand = new Command(async () => await SelectAttachment());

        }

        private async Task Send()
        {
            if (String.IsNullOrWhiteSpace(MessageToSend))
                return;

            var chatMessage = new ChatItem(MessageToSend);

            chatMessage.Date = DateTimeOffset.Now;
            chatMessage.ItemType = ChatItem.ChatItemType.Outgoing;
            chatMessage.Status = ChatItem.ChatItemStatus.Read;

            App.ChatManager.AddChatItem(Chat.Id, chatMessage);

            MessageToSend = "";

            OnChatItemAdded?.Invoke(chatMessage);

        }

        private async Task SelectAttachment()
        {
            await CrossMedia.Current.Initialize();

            var cameraStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
            var storageStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);

            if (cameraStatus != PermissionStatus.Granted || storageStatus != PermissionStatus.Granted)
            {
                var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Camera, Permission.Storage });
                cameraStatus = results[Permission.Camera];
                storageStatus = results[Permission.Storage];

                //var stream = file.GetStream();

            }
            else
            {

                var file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions()
                {
                    CompressionQuality = 70
                });
                

                if (file != null)
                    await PushModalAsync(new AttachmentPage(file.Path,this));

            }



        }

    }
}
