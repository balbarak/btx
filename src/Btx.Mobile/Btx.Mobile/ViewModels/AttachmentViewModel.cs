﻿using Btx.Mobile.Models;
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

        public byte[] FileData { get; set; }

        public ICommand SendCommand { get; }

        public ICommand CloseCommand { get; }

        private string attachmentFilePath;
        public string AttachmentFilePath
        {
            get { return attachmentFilePath; }
            set { attachmentFilePath = value; OnPropertyChanged(); }
        }

        public AttachmentViewModel(byte[] fileData,string path,ChatBoxViewModel chatBox)
        {
            this.AttachmentFilePath = path;
            this.ChatBox = chatBox;
            this.FileData = fileData;

            SendCommand = new Command(Send);
            CloseCommand = new Command(Close);
        }

        public void Send()
        {
            PopModalAsync();

            var chatMessage = new ChatItemViewModel(ChatItemType.OutgoingFile)
            {
                Body = "ss",
                LocalFilePath = AttachmentFilePath
            };
           
            App.ChatManager.AddChatItem(ChatBox.Chat.Id, chatMessage);
            
            
        }

        public void Close()
        {
            PopModalAsync();
        }
    }
}
