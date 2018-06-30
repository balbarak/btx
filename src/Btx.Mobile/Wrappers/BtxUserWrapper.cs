using Btx.Client.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Btx.Mobile.Wrappers
{
    public class BtxUserWrapper : WrapperBase<BtxUser>
    {
        public string Id { get { return GetValue<string>(); } set { SetValue(value); } }

        public string Username { get { return GetValue<string>(); } set { SetValue(value); } }

        public string Nickname { get { return GetValue<string>(); }set { SetValue(value); } }

        public BtxUserWrapper(BtxUser model) : base(model)
        {

        }
    }
}
