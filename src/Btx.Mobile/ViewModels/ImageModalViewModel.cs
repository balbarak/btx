using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Btx.Mobile.ViewModels
{
    public class ImageModalViewModel : BaseViewModel
    {
        public ICommand CloseCommand { get; set; }

        public ImageSource Source
        {
            get
            {
                if (ImageBytes != null)
                    return ImageSource.FromStream(() => new MemoryStream(ImageBytes));

                if (!String.IsNullOrWhiteSpace(ImageFilePath))
                    return ImageSource.FromFile(ImageFilePath);

                return null;
            }
        }

        public byte[] ImageBytes { get; set; }

        private string imageFilePath;

        public string ImageFilePath
        {
            get { return imageFilePath; }
            set { imageFilePath = value; OnPropertyChanged(); }
        }

        public ImageModalViewModel(string path,byte[] imageBytes)
        {
            CloseCommand = new Command(async () => await Close());

            ImageFilePath = path;

            ImageBytes = imageBytes;
        }

        private async Task Close()
        {
            await PopupNavigation.Instance.PopAsync(true);
        }
    }
}
