using Btx.Mobile.ControlEventArgs;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Btx.Mobile.Controls
{
    public class ChatBoxListView : ListView
    {
        public event EventHandler OnScroll;

        public ChatBoxListView() : base(ListViewCachingStrategy.RecycleElement)
        {

        }

        public virtual void OnScrollInternal(object sender, ChatBoxListEventArgs e)
        {
            OnScroll?.Invoke(sender, e);
        }

       
    }
}
