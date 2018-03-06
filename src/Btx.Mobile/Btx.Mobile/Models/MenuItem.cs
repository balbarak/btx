using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Btx.Mobile.Models
{
    public class BtxMenuItem : ObservableObject
    {
        public string Title { get; set; }

        public string Icon { get; set; }

        public string PageName { get; set; }

        public BtxMenuItem()
        {

        }

        public BtxMenuItem(string title,string icon,string pageName)
        {
            this.Title = title;
            this.Icon = icon;
            this.PageName = pageName;
        }
    }
}
