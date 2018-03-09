using Btx.Mobile.CustomRenders;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Btx.Mobile.iOS.CustomRenders;


[assembly: ExportRenderer(typeof(ChatEditBox), typeof(ChatEditBoxRenderer))]
namespace Btx.Mobile.iOS.CustomRenders
{
    public class ChatEditBoxRenderer : EditorRenderer
    {
        
        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                //Control.SetBackgroundColor(Color.Transparent.ToAndroid());

                //Control.KeyboardType = UIKit.UIKeyboardType.Default;
                //Control.KeyboardAppearance = UIKit.UIKeyboardAppearance.Default;

                Control.ScrollEnabled = true;
            }

        }
    }
}
