using System;
using System.Collections.Generic;
using System.Text;

namespace Btx.Mobile.ControlEventArgs
{
    public class ChatBoxListEventArgs : EventArgs
    {
        public int TotalItemsCount { get; set; }

        public int FirstItemIndex { get; set; }

        public int VisibleItemCount { get; set; }

        public ChatBoxListEventArgs()
        {

        }
    }
}
