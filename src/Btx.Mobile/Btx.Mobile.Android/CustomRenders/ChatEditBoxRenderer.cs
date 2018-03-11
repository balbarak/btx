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
using Btx.Mobile.Controls;
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
            this.LayoutParameters = new ViewGroup.LayoutParams(0, ViewGroup.LayoutParams.WrapContent);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                
                Control.VerticalScrollBarEnabled = true;
                Control.VerticalFadingEdgeEnabled = true;
                Control.SetPadding(40, 30, 30, 30);
                Control.SetEms(13);
                Control.SetMaxLines(5);
                Control.SetBackgroundColor(Color.White.ToAndroid());
                Control.Hint = "Send a message ...";
                Control.SetSingleLine(false);

            }

        }
    }
}