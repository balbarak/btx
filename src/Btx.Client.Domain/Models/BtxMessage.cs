using System;
using System.Collections.Generic;
using System.Text;

namespace Btx.Client.Domain.Models
{
    public class BtxMessage
    {
        public string Id { get; private set; }

        public string FromUserId { get; set; }

        public string ToUserId { get; set; }

        public string Body { get; set; }

        public BtxMessageStatus Status { get; set; }

        public DateTimeOffset Date { get; set; }
        
        public BtxMessage()
        {
            Id = Guid.NewGuid().ToString();
            Status = BtxMessageStatus.Pending;
        }
        
    }
}
