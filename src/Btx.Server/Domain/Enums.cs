using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Btx.Server.Domain
{
    public enum MessageStatus
    {
        ServerDelivered = 0,
        UserDelivered = 1,
        UserRead = 2
    }
}
