using System;
using System.Collections.Generic;
using System.Text;

namespace Btx.Client.Models
{
    public class BtxMessage
    {
        public string Id { get; set; }

        public string Body { get; set; }

        public DateTime Date { get; set; }

        public BtxMessage()
        {
            Id = Guid.NewGuid().ToString();
            Date = DateTime.Now;
        }
    }
}
