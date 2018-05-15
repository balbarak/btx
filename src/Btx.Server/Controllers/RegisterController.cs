using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Btx.Client.Domain.Models;
using Btx.Server.Domain;
using Btx.Server.Identity;
using Btx.Server.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Btx.Server.Controllers
{
    public class RegisterController : BaseApiController
    {
        private ILogger<RegisterController> _logger;

        public RegisterController(ILogger<RegisterController> logger,IConfiguration config, BtxUserManager userManager) : base(config,userManager)
        {
            _logger = logger;
        }

        public IActionResult Get()
        {
            return Ok("It is work!");
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]BtxRegister model)
        {
            try
            {
                var user = new User(model);

                var result = await UserService.Instance.Register(user, model.Password);

                if (!result.Succeeded)
                    return BadRequest(result.Errors.Select(a=> new[] { a.Code, a.Description }));

                var jwtToken = GetJwtSecurityToken(user);
                var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

                token = token.Replace("\"", "");

                return Ok(token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());

                return BadRequest(ex);
            }

        }
    }
}