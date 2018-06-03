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
	public partial class LogoutPage : ContentPage
	{
        protected LogoutViewModel ViewModel => BindingContext as LogoutViewModel;

        public LogoutPage ()
		{
			InitializeComponent();

            this.BindingContext = new LogoutViewModel();
        }
	}
}