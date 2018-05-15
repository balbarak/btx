using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Btx.Server.Domain;
using Btx.Server.Helper;
using Btx.Server.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Btx.Server.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    public class BaseApiController : Controller
    {
        protected IConfiguration _configuration;
        protected BtxUserManager _userManager;

        public BaseApiController(IConfiguration config,BtxUserManager userManager)
        {
            _configuration = config;
            _userManager = userManager;
            
        }
        
        protected JwtSecurityToken GetJwtSecurityToken(User user)
        {
            var claims = _userManager.GetClaimsAsync(user).GetAwaiter().GetResult();
            
            var keyStr = _configuration[WebConstants.TOKEN_KEY];

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyStr)); //new SymmetricSecurityKey(Encoding.UTF8.GetBytes("{BB40E6B8-4DC9-46FC-B74F-78A34A333876}"));;

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var aud = _configuration[WebConstants.TOKEN_ISSUER];

            return new JwtSecurityToken
                (
                    issuer: _configuration[WebConstants.TOKEN_ISSUER],
                    audience: _configuration[WebConstants.TOKEN_AUDIENCE],
                    claims: claims,
                    expires: DateTime.Now.AddMonths(2),
                    signingCredentials: creds
                );
        }
    }
}