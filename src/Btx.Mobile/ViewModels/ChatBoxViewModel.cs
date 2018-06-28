using Btx.Client.Application.Services;
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
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;
using Btx.Client.Domain.Search;
using Btx.Mobile.Services;
using System.Collections.ObjectModel;

namespace Btx.Mobile.ViewModels
{

    public class ChatBoxViewModel : BaseViewModel
    {
        private int _page = 1;
        private int _totalItemsCount = -1;

        private CancellationTokenSource _ctk = new CancellationTokenSource();

        public bool HasItemToLoads { get { return _totalItemsCount != Items.Count; } }

        public BtxThreadWrapper BtxThread { get; private set; }

        public ObservableCollection<BtxMessageWrapper> Items { get; private set; } = new ObservableCollection<BtxMessageWrapper>();

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

            CacheHelper.CurrenChatBoxViewModel = this;

            SendCommand = new Command(async () => { await Send(); });
            SelectImageCommand = new Command(async () => await SelectImage());

            this.Title = BtxThread.Title;
        }
        
        public override async Task OnAppearing()
        {
            await LoadMessages();

            var chatListViewModel = ServiceLocator.Current.GetService<ChatListViewModel>();

            chatListViewModel.ReadAllMessages(BtxThread.Id);

        }

        public override Task OnDisappearing()
        {
            _ctk.Cancel();

            return base.OnDisappearing();
        }

        private async Task Send()
        {
            if (String.IsNullOrWhiteSpace(MessageToSend))
                return;

            var chatMessage = new BtxMessage()
            {
                Body = messageToSend,
                Date = DateTime.UtcNow,
                BtxMessageType = BtxMessageType.Outgoing,
                IsReadByUser = true,
                ThreadId = BtxThread.Id,
                RecipientId = BtxThread.Id
            };

            Items.Add(new BtxMessageWrapper(chatMessage));

            await BtxProtocolService.Instance.SendMessage(chatMessage);
            
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
                    await PushModalAsync(new SelectedImagePage(null, file.Path, this));

            }

        }

        public async Task LoadMessages(bool isAppendTop = false)
        {
            
            if (IsBusy || !HasItemToLoads)
                return;

            IsBusy = true;

            var search = new SearchCriteria<BtxMessage>()
            {
                FilterExpression = a => a.ThreadId == BtxThread.Id,
                PageSize = 20,
                PageNumber = _page,
                SortExpression = a => a.OrderByDescending(p => p.Date)
            };

            var result = await BtxMessageService.Instance.Search(search);

            _totalItemsCount = result.TotalResultsCount;

            int index = 1;

            if (!isAppendTop)
                result.Result.Reverse();

            foreach (var item in result.Result)
            {
                
                BtxMessageWrapper wrapper = new BtxMessageWrapper(item);

                Debug.WriteLine($"Reading msg from db {index}");

                if (_ctk.IsCancellationRequested)
                    break;

                if (isAppendTop)
                    Items.Insert(0, wrapper);
                else
                    Items.Add(wrapper);

                index++;
            }

            if (result.Result.Any())
                _page++;

            IsBusy = false;

        }

        public void UpdateChatMessage(BtxMessage msg)
        {
            var found = Items.Where(a => a.Model.Id == msg.Id).FirstOrDefault();

            if (found != null)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    found.Status = msg.Status;
                });
                
            }
        }

    }

}
