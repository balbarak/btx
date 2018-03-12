using Btx.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Btx.Mobile.ViewModels
{
    public class AttachmentViewModel : BaseViewModel
    {
        public ChatBoxViewModel ChatBox { get; set; }

        public ICommand SendCommand { get; }

        private string attachmentPath;
        public string AttachmentPath
        {
            get { return attachmentPath; }
            set { attachmentPath = value; OnPropertyChanged(); }
        }

        public AttachmentViewModel(string path,ChatBoxViewModel chatBox)
        {
            this.AttachmentPath = path;
            this.ChatBox = chatBox;

            SendCommand = new Command(Send);
        }

        public void Send()
        {
            PopModalAsync();

            var item = new ChatItem()
            {
                ItemType = ChatItem.ChatItemType.OutgoingFile,
                Body = "File sent"
            };

            ChatBox.Items.Add(item);

            ChatBox.InvokeOnChatItemAdded(item);
        }
    }
}
