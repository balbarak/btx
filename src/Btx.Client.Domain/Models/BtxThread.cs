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

        public BtxThread()
        {

        }

        public BtxThread(BtxMessage msg)
        {
            Id = msg.Id;
            Title = msg.Recipient?.Username;
        }
    }
}
