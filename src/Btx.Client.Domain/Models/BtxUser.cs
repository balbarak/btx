using System;
using System.Collections.Generic;
using System.Text;

namespace Btx.Client.Domain.Models
{
    public class BtxUser
    {
        public string Id { get; set; }

        public string Nickname { get; set; }

        public string Username { get; set; }

        public BtxUser()
        {

        }

        public BtxUser Update(BtxUser entity)
        {
            this.Username = entity.Username;
            this.Nickname = entity.Nickname;

            return this;
        }

    }
}
