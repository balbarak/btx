using Btx.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace Btx.Mobile.Services
{
    public class BtxProtocolService
    {
        public static BtxProtocolService Instance { get; }

        private BtxClient _client;

        public BtxClient Client
        {
            get
            {
                if (_client == null)
                    _client = new BtxClient();

                return _client;
            }
            private set { _client = value; }
        }

        static BtxProtocolService()
        {
            Instance = new BtxProtocolService();
        }

    }
}
