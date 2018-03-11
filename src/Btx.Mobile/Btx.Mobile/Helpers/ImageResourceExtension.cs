
using FFImageLoading.Forms;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Btx.Mobile.Helpers
{
    public class ImageResourceExtension : IMarkupExtension
    {
        public string Image { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Image == null)
            {
                return null;
            }
            // Do your translation lookup here, using whatever method you require
            
            var imageSource = ImageSource.FromResource($"Btx.Mobile.Images.{Image}");

            

            return new EmbeddedResourceImageSource(new Uri(Image)); 
        }
    }


}
