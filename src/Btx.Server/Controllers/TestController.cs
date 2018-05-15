using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Btx.Server.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Btx.Server.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TestController : BaseApiController
    {
        public TestController(IConfiguration config, BtxUserManager userManager) : base(config, userManager)
        {
            
        }

        public IActionResult Get()
        {
            return Ok("It is work!");
        }

    }
}