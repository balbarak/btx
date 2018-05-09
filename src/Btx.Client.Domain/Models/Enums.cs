using System;
using System.Collections.Generic;
using System.Text;

namespace Btx.Client.Domain.Models
{
    public enum BtxMessageType
    {
        Incoming = 0,
        Outgoing = 1,
        Info = 2,
        OutgoingImage = 3,
        IncomingImage = 4
    }

    public enum BtxMessageStatus
    {
        Pending = 0,
        ServerDeliverd = 1,
        UserDeliverd = 2,
        Read = 3
    }

}
