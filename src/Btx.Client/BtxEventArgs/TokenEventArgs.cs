using System;
using System.Collections.Generic;
using System.Text;

namespace Btx.Client.BtxEventsArg
{
    public class TokenEventArgs : EventArgs
    {
        public string Token { get; private set; }

        public string Username { get; private set; }

        public TokenEventArgs(string token,string username)
        {
            this.Token = token;
            this.Username = username;   

        }
    }
}
