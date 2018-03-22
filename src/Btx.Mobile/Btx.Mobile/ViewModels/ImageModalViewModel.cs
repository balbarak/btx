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

        private string imageFilePath;

        public string ImageFilePath
        {
            get { return imageFilePath; }
            set { imageFilePath = value; OnPropertyChanged(); }
        }
        
        public ImageModalViewModel(string path)
        {
            CloseCommand = new Command(async ()=> await Close());

            ImageFilePath = path;
        }
        
        private async Task Close()
        {
            await PopupNavigation.Instance.PopAsync(true);
        }
    }
}
