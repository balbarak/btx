using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Btx.Mobile.ViewModels
{
    public class ImageModalViewModel : BaseViewModel
    {
        public ICommand CloseCommand { get; set; }

        public ImageModalViewModel()
        {
            CloseCommand = new Command(async ()=> await Close());
        }

        private async Task Close()
        {
            await PopupNavigation.Instance.PopAsync(true);
        }
    }
}
