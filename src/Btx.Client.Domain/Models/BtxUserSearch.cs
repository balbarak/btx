using Btx.Client.Domain.Search;
using System;
using System.Collections.Generic;
using System.Text;

namespace Btx.Client.Domain.Models
{
    public class BtxUserSearch : SearchBase
    {
        public string Username { get; set; }

        public string Nickname { get; set; }
    }
}
