using Btx.Client.Domain.Models;
using Btx.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Btx.Mobile.Wrappers
{
    public class BtxThreadWrapper : WrapperBase<BtxThread>
    {
        public string Id
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string Title
        {
            get
            {
                return GetValue<string>();
            }
            set
            {
                SetValue(value);
            }
        }
        

        public BtxThreadWrapper(BtxThread model) : base(model)
        {

        }
    }
}
