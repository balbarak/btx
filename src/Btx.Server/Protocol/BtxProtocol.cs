using Btx.Client.Domain;
using Btx.Client.Domain.Models;
using Btx.Server.Domain;
using Btx.Server.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Btx.Server.Protocol
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BtxProtocol : Hub
    {
        public string UserId
        {
            get
            {
                return this.Context.User.FindFirst(ClaimTypes.Sid)?.Value;
            }
        }

        public override Task OnConnectedAsync()
        {
            AddNewConnection();

            return base.OnConnectedAsync();
        }
        
        public override Task OnDisconnectedAsync(Exception exception)
        {

            SetOffline();

            return base.OnDisconnectedAsync(exception);
        }

        public async Task Send(BtxMessage msg)
        {

            var activeConnections = ConnectionService.Instance.GetActiveConnections(msg.RecipientId);

            var newMessage = new BtxMessage()
            {
                RecipientId = UserId,
                Body = msg.Body,
            };

            foreach (var item in activeConnections)
            {
                await Clients.Client(item.Id).SendAsync(ClientMethods.ON_MESSAGE_RECIEVE, msg);
            }
        }

        private void SetOffline()
        {
            var connection = ConnectionService.Instance.GetById(Context.ConnectionId);

            if (connection != null)
            {
                connection.IsConnected = false;
                ConnectionService.Instance.Update(connection);
            }
        }

        private void AddNewConnection()
        {
            var connection = new Connection(UserId, Context.ConnectionId);

            ConnectionService.Instance.Add(connection);
        }
        

        
    }
}
