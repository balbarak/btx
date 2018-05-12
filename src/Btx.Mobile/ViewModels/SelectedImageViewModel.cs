using Btx.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Btx.Mobile.ViewModels
{
    public class SelectedImageViewModel : BaseViewModel
    {
        public ChatBoxViewModel ChatBox { get; set; }

        public byte[] FileData { get; set; }

        public ICommand SendCommand { get; }

        public ICommand CloseCommand { get; }

        private string imageFilePath;
        public string ImageFilePath
        {
            get { return imageFilePath; }
            set { imageFilePath = value; OnPropertyChanged(); }
        }

        public SelectedImageViewModel(byte[] fileData, string path, ChatBoxViewModel chatBox)
        {
            this.ImageFilePath = path;
            this.ChatBox = chatBox;
            this.FileData = fileData;

            SendCommand = new Command(Send);
            CloseCommand = new Command(Close);
        }

        public void Send()
        {
            PopModalAsync();

            //var chatMessage = new ImageItemViewModel(ChatItemType.OutgoingImage,ImageFilePath)
            //{
            //    Body = "ss",
            //    LocalFilePath = ImageFilePath
            //};

            //App.ChatManager.AddChatItem(ChatBox.BtxThread.Id, chatMessage);


        }

        public void Close()
        {
            PopModalAsync();
        }
    }
}
