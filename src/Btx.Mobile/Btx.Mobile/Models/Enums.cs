using System;
using System.Collections.Generic;
using System.Text;

namespace Btx.Mobile.Models
{
    public enum ChatItemStatus
    {
        Pending = 0,
        ServerDeliverd = 1,
        UserDeliverd = 2,
        Read = 3
    }

    public enum ChatItemType
    {
        Incoming = 0,
        Outgoing = 1,
        Info = 2,
        OutgoingFile = 3,
    }
}
