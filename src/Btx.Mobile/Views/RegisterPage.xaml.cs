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
	public partial class RegisterPage : ContentPage
	{
        protected RegisterViewModel ViewModel => BindingContext as RegisterViewModel;

		public RegisterPage ()
		{
			InitializeComponent ();

            this.BindingContext = new RegisterViewModel();
		}
	}
}