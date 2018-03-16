
using FFImageLoading.Forms;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Btx.Mobile.Helpers
{
    [ContentProperty(nameof(ImageResourceExtension.Source))]
    public class ImageResourceExtension : IMarkupExtension
    {
        public string Source { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Source == null)
            {
                return null;
            }
            // Do your translation lookup here, using whatever method you require
            
            var result = ImageSource.FromResource($"Btx.Mobile.Images.{Source}");

            return result;
        }
    }


}
