using Btx.Mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Btx.Mobile.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AttachmentPage : ContentPage
	{
        protected AttachmentViewModel ViewModel => BindingContext as AttachmentViewModel;

        public AttachmentPage (string path,ChatBoxViewModel chatBox)
		{
			InitializeComponent ();

            this.BindingContext = new AttachmentViewModel(path, chatBox);
        }
	}
}