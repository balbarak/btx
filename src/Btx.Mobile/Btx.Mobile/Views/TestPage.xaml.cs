using Btx.Mobile.Views.Modals;
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
	public partial class TestPage : ContentPage
	{
		public TestPage ()
		{
			InitializeComponent ();
		}

        private void OnClick(object sender,EventArgs args)
        {
            Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(new ImageModalPage(), true);

            //Rg.Plugins.Popup.Services.PopupNavigation.PushAsync(true);
        }
	}
}