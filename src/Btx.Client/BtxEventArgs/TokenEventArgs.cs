using System;
using System.Collections.Generic;
using System.Text;

namespace Btx.Client.BtxEventsArg
{
    public class TokenEventArgs : EventArgs
    {
        public string Token { get; private set; }

        public TokenEventArgs(string token)
        {
            this.Token = token;
        }
    }
}
