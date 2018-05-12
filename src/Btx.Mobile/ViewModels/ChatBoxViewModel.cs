﻿using Btx.Client.Application.Services;
using Btx.Client.Domain.Models;
using Btx.Mobile.Helpers;
using Btx.Mobile.MockData;
using Btx.Mobile.Models;
using Btx.Mobile.Views;
using Btx.Mobile.Wrappers;
using MvvmHelpers;
using Plugin.FilePicker;
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
    
    public class ChatBoxViewModel : BaseViewModel
    {
        public BtxThreadWrapper BtxThread { get; private set; }

        public ObservableRangeCollection<BtxMessageWrapper> Items { get { return BtxThread.Messages; } } 
        private string messageToSend;

        public string MessageToSend
        {
            get { return messageToSend; }
            set { messageToSend = value; OnPropertyChanged(); }
        }

        public ICommand SendCommand { get; }

        public ICommand SelectImageCommand { get; }

        public ChatBoxViewModel()
        {
            BtxThread = App.ChatManager.CurrentThread;

            SendCommand = new Command(async () => { await Send(); });
            SelectImageCommand = new Command(async () => await SelectImage());

            this.Title = BtxThread.Title;

            LoadMessages();
            

        }
        
        private async Task Send()
        {
            if (String.IsNullOrWhiteSpace(MessageToSend))
                return;

            var chatMessage = new BtxMessage()
            {
                Body = messageToSend,
                Date = DateTime.Now,
                BtxMessageType = BtxMessageType.Outgoing,
                IsReadByUser = true
            };

            Items.Add(new BtxMessageWrapper(chatMessage));
            
            MessageToSend = "";
            
            
        }

        private async Task SelectImage()
        {
            await CrossMedia.Current.Initialize();

            var cameraStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
            var storageStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);

            if (cameraStatus != PermissionStatus.Granted || storageStatus != PermissionStatus.Granted)
            {
                var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Camera, Permission.Storage });
                cameraStatus = results[Permission.Camera];
                storageStatus = results[Permission.Storage];
                
            }
            else
            {
                var file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions()
                {
                    CompressionQuality = 70
                });
                
                if (file != null)
                    await PushModalAsync(new SelectedImagePage(null,file.Path, this));

            }

        }

        private void LoadMessages()
        {
            var msgs = BtxMessageService.Instance.GetByThreadId(BtxThread.Id);

            foreach (var item in msgs)
            {
                BtxMessageWrapper wrapper = new BtxMessageWrapper(item);

                Items.Add(wrapper);
            }
        }

    }
}
