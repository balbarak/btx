using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Btx.Client.Domain.Models;
using Btx.Server.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Btx.Server.Controllers
{
    public class LoginController : BaseApiController
    {
        private ILogger _logger;
        private BtxSignInManager _signInManager;

        public LoginController(BtxSignInManager signInManager, ILogger<LoginController> logger,IConfiguration config, BtxUserManager userManager): base(config,userManager)
        {
            _logger = logger;
            _signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]BtxLogin model)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(model.Username);

                if (user == null)
                    throw new Exception("user not found");

                var result = await _signInManager.PasswordSignInAsync(user, model.Password,false,true);

                if (!result.Succeeded || result.IsLockedOut || result.IsNotAllowed)
                    throw new Exception("Authentication failed");
                
                var jwtToken = GetJwtSecurityToken(user);
                var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

                token = token.Replace("\"", "");

                return Ok(token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());

                return BadRequest();
            }
        }
    }
}