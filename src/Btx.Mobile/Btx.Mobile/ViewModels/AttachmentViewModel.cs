using System;
using System.Collections.Generic;
using System.Text;

namespace Btx.Mobile.ViewModels
{
    public class AttachmentViewModel : BaseViewModel
    {
        private string attachmentPath;
        public string AttachmentPath
        {
            get { return attachmentPath; }
            set { attachmentPath = value; OnPropertyChanged(); }
        }

        public AttachmentViewModel(string path)
        {
            this.AttachmentPath = path;
        }
    }
}
