using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Btx.Client.Wpf.Helpers
{
    public class ChatSimulator
    {
        private BtxClient _client;
        private Logger _logger;


        public ChatSimulator(BtxClient client,Logger logger)
        {
            _client = client;
            _logger = logger;
        }

        public async Task SendRandomMessages(string to,int count = 10)
        {
            var message = ChatGenerator.GetRandomMessagesToSend(to, count);

            foreach (var item in message)
            {
                _logger.LogInformation($"Sending messate to : {to}");

                await _client.Send(item).ConfigureAwait(false);
            }

        }
    }
}
