using Btx.Mobile.ViewModels;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Btx.Mobile.Views.Modals
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ImageModalPage : PopupPage
	{
		public ImageModalPage(string path,byte[] imageBytes)
		{
			InitializeComponent ();

            BindingContext = new ImageModalViewModel(path,imageBytes);
		}
        
	}
}