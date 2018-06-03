using System;
using System.Collections.Generic;
using System.Text;

namespace Btx.Client.Exceptions
{
    
    public class BtxClientException : Exception
    {
        public BtxClientException() { }
        public BtxClientException(string message) : base(message) { }
        public BtxClientException(string message, Exception inner) : base(message, inner) { }
        protected BtxClientException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
    
}
