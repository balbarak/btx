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
            AddMessageToDatabase(msg);

            await Clients.Caller.SendAsync(ClientMethods.ON_MESSAGE_SERVER_DELIEVERED, msg.Id);

            var activeConnections = ConnectionService.Instance.GetActiveConnections(msg.RecipientId);

            var newMessage = new BtxMessage()
            {
                Id = msg.Id,
                RecipientId = UserId,
                Recipient = new BtxUser() { Username = Context.User.Identity.Name },
                Date = DateTime.UtcNow,
                Body = msg.Body,
            };
            
            foreach (var item in activeConnections)
            {
                await Clients.Client(item.Id).SendAsync(ClientMethods.ON_MESSAGE_RECIEVE, newMessage);
            }

        }

        public async Task MessageDelivered(string id)
        {
            var msg = MessageService.Instance.GetById(id);

            if (msg == null)
                return;

            msg = msg.SetUserDelivered();

            var activeConnections = ConnectionService.Instance.GetActiveConnections(msg.FromUserId);

            foreach (var item in activeConnections)
            {
                await Clients.Client(item.Id).SendAsync(ClientMethods.ON_MESSAGE_USER_DELIEVERED, msg.Id);
            }

            if (activeConnections.Any())
                msg.IsUserDeliveredNotified = true;

            MessageService.Instance.Update(msg);
        }

        private void AddMessageToDatabase(BtxMessage msg)
        {
            var fromUserId = UserId;
            var toUserId = msg.RecipientId;
            var serverMsg = new Message(msg.Id)
            {
                Body = msg.Body,
                Status = MessageStatus.ServerDelivered,
                FromUserId = fromUserId,
                ToUserId = toUserId
            };
            MessageService.Instance.Add(serverMsg);
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
