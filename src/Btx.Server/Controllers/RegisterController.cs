using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Btx.Client.Domain.Models;
using Btx.Server.Domain;
using Btx.Server.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Btx.Server.Controllers
{
    public class RegisterController : BaseApiController
    {
        public RegisterController(IConfiguration config, BtxUserManager userManager) : base(config,userManager)
        {

        }

        public IActionResult Get()
        {
            return Ok("It is work!");
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]RegisterForm model)
        {
            try
            {
                var user = new User(model);

                var result = await _userManager.CreateAsync(user, model.Password);

                if (!result.Succeeded)
                    return BadRequest(result.Errors.Select(a=> new[] { a.Code, a.Description }));

                var jwtToken = GetJwtSecurityToken(user);
                var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

                return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
    }
}