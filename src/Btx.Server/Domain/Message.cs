using Btx.Client.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Btx.Server.Domain
{
    public class Message
    {
        public string Id { get; set; }

        public string FromUserId { get; set; }

        [ForeignKey(nameof(FromUserId))]
        public User FromUser { get; set; }

        public string ToUserId { get; set; }

        [ForeignKey(nameof(ToUserId))]
        public User ToUser { get; set; }

        public DateTime Date { get; set; }

        public string Body { get; set; }

        public MessageStatus Status { get; set; }

        public bool IsUserDeliveredNotified { get; set; }

        public Message()
        {
            Date = DateTime.UtcNow;
        }

        public Message(string id) : this()
        {
            this.Id = id;
        }

        public Message SetUserDelivered()
        {
            this.Status = MessageStatus.UserDelivered;

            return this;
        }

        public BtxMessage ToBtxMessage()
        {
            return new BtxMessage()
            {
                Id = this.Id,
                Body = this.Body,
                Recipient = new BtxUser()
                {
                    Id = this.FromUserId,
                    Username = this.FromUser?.UserName,
                },
                Date = this.Date,
                RecipientId = this.FromUserId
            };
        }

    }
}
