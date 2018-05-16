using Btx.Client.Domain.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Btx.Server.Domain
{
    public class User : IdentityUser
    {
        public List<Connection> Connections { get; set; }

        public string PublicKey { get; set; }

        public User()
        {

        }

        public User(BtxRegister registeration)
        {
            this.UserName = registeration.Username;
            this.Email = "btx@user.com";
        }
    }
}
