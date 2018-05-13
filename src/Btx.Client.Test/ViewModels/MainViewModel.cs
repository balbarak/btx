using Btx.Client.Test.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Btx.Client.Test.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public BtxClient Client { get; private set; }

        public ICommand ConnectCommand { get; }

        public string LogMessages
        {
            get
            {
                return LoggerProvider.CurrentLogger.LogMessages;
            }
        }

        public MainViewModel()
        {
            
            Client = new BtxClient(LoggerProvider);

            LoggerProvider.CurrentLogger.OnWriteLog += CurrentLogger_OnWriteLog;

            ConnectCommand = new RelayCommand(
                async (obj) =>
                {
                    await Connect();
                });
        }

        private void CurrentLogger_OnWriteLog(object sender, EventArgs e)
        {
            OnPropertyChanged(nameof(LogMessages));
        }

        private async Task Connect()
        {
            await Client.Connect();
        }

    }
}
