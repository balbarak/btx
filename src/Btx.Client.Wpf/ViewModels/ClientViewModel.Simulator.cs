using Btx.Client.Wpf.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Threading;

namespace Btx.Client.Wpf.ViewModels
{
    public partial class ClientViewModel
    {
        private CancellationTokenSource _randomToken = new CancellationTokenSource();

        private bool _isBusySendingRandomMessage;

        public bool IsBusySendingRandomMessage
        {
            get { return _isBusySendingRandomMessage; }
            set
            {
                _isBusySendingRandomMessage = value;
                CanCancellRandomMessage = !value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(CanCancellRandomMessage));
                RaisePropertyChanged(nameof(CanSendRandomMessage));
            }
        }

        public bool CanCancellRandomMessage { get; set; }

        private int _randomMessageCount = 10;

        public int RandomMessageCount
        {
            get { return _randomMessageCount; }
            set { _randomMessageCount = value; RaisePropertyChanged(); }
        }

        private ICommand _sendRandomMessageCommand;

        public ICommand SendRandomMessageCommand
        {
            get
            {
                if (_sendRandomMessageCommand == null)
                    _sendRandomMessageCommand = new RelayCommand(async (arg) => { await SendRandomMessage(); });

                return _sendRandomMessageCommand;
            }

        }

        private ICommand _sendCounterMessagesCommand;

        public ICommand SendCounterMessagesCommand
        {
            get
            {
                if (_sendCounterMessagesCommand == null)
                    _sendCounterMessagesCommand = new RelayCommand(async (arg) => { await SendCounterMessage(); });

                return _sendCounterMessagesCommand;
            }
        }

        private int _counterMessageCount = 100;

        public int CounterMessageCount
        {
            get { return _counterMessageCount; }
            set { _counterMessageCount = value; RaisePropertyChanged(); }
        }


        public bool CanSendRandomMessage
        {
            get
            {
                return IsConnected && SelectedBtxUser != null && !IsBusySendingRandomMessage;
            }
        }

        private async Task SendRandomMessage()
        {
            IsBusySendingRandomMessage = true;

            ChatSimulator simulator = new ChatSimulator(Client, LoggerProvider.CurrentLogger);

            await simulator.SendRandomMessages(SelectedBtxUser.Id, RandomMessageCount);

            IsBusySendingRandomMessage = false;
            
        }

        private async Task SendCounterMessage()
        {
            IsBusySendingRandomMessage = true;

            ChatSimulator simulator = new ChatSimulator(Client, LoggerProvider.CurrentLogger);

            await simulator.SendCounterMessages(SelectedBtxUser.Id, CounterMessageCount);

            IsBusySendingRandomMessage = false;

        }

    }
}
