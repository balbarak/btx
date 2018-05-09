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
	public partial class SelectedImagePage : ContentPage
	{
        protected SelectedImageViewModel ViewModel => BindingContext as SelectedImageViewModel;

        public SelectedImagePage (byte[] data,string path,ChatBoxViewModel chatBox)
		{
			InitializeComponent ();

            this.BindingContext = new SelectedImageViewModel(data, path, chatBox);
        }
	}
}