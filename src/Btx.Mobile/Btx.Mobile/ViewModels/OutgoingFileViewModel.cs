using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Btx.Mobile.ViewModels
{
    public class OutgoingFileViewModel : BaseViewModel
    {
        public ICommand SendCommand { get; }

        public OutgoingFileViewModel()
        {
            SendCommand = new Command(Send);
        }


        public void Send()
        {

        }
    }
}
