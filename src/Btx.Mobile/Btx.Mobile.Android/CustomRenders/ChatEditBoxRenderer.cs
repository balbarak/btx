using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Btx.Mobile.CustomRenders;
using Btx.Mobile.Droid.CustomRenders;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;


[assembly: ExportRenderer(typeof(ChatEditBox), typeof(ChatEditBoxRenderer))]
namespace Btx.Mobile.Droid.CustomRenders
{
    public class ChatEditBoxRenderer : EditorRenderer
    {
        public ChatEditBoxRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.SetBackgroundColor(Color.Transparent.ToAndroid());
                Control.Hint = "Send a message ...";
                
            }

        }
    }
}