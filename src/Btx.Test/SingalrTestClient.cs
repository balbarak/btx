using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Btx.Test
{
    [TestClass]
    public class SingalrTestClient
    {
        [TestMethod]
        public void Connect()
        {

            HubConnectionBuilder builder = new HubConnectionBuilder();
            builder.WithUrl("http://localhost:5000/btx");

            var hub = builder.Build();

            hub.StartAsync();
        }
    }
}
