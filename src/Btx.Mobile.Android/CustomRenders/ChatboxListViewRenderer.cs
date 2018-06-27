using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Btx.Mobile.ControlEventArgs;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Btx.Mobile.Controls;
using Btx.Mobile.Droid.CustomRenders;
using Xamarin.Forms.Platform.Android;

[assembly: Xamarin.Forms.ExportRenderer(typeof(ChatBoxListView), typeof(ChatboxListViewRenderer))]
namespace Btx.Mobile.Droid.CustomRenders
{
    public class ChatboxListViewRenderer : ListViewRenderer
    {
        public ChatboxListViewRenderer(Context context) : base(context)
        {

        }

        
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.ListView> e)
        {
            base.OnElementChanged(e);

            Control.Scroll += OnScroll;
        }

        private void OnScroll(object sender, AbsListView.ScrollEventArgs e)
        {
            if (Element is ChatBoxListView chatBox)
            {
                chatBox.OnScrollInternal(this,new ChatBoxListEventArgs()
                {
                    FirstItemIndex = e.FirstVisibleItem,
                    TotalItemsCount = e.TotalItemCount,
                    VisibleItemCount = e.VisibleItemCount
                });
            }
        }


        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            
        }
    }
}