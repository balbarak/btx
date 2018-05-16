using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Btx.Server.Domain
{
    public class Connection
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        public string IPAddress { get; set; }

        public string UserAgent { get; set; }

        public string ClientVersion { get; set; }

        public DateTime? Date { get; set; }

        public bool IsConnected { get; set; }

        public Connection()
        {
            Date = DateTime.Now;
            IsConnected = true;
        }

        public Connection(string userId,string connectionId) : this()
        {
            this.Id = connectionId;
            UserId = userId;
        }
    }
}
