using System;
using System.Collections.Generic;
using System.Text;

namespace Btx.Client.Domain.Models
{
    public class BtxThread
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public List<BtxMessage> Messages { get; set; }


    }
}
