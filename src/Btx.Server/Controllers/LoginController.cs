using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Btx.Server.Controllers
{
    public class LoginController : BaseApiController
    {
        [HttpPost]
        public IActionResult Post()
        {
            return View();
        }
    }
}