using Btx.Client.Domain.Search;
using Btx.Server.Domain;
using Btx.Server.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Btx.Server.Services
{
    public class UserService : ServiceBase<UserService>
    {
        public async Task<IdentityResult> Register(User user, string password)
        {
            IdentityResult result;

            using (BtxUserManager userManager = BtxUserManager.Create())
            {

                userManager.Logger.LogInformation("registering new user: {0}", user.UserName);

                result = await userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    
                    Claim nameClaim = new Claim(ClaimTypes.Name, user.UserName);
                    Claim idClaim = new Claim(ClaimTypes.Sid, user.Id);

                    await userManager.AddClaimsAsync(user,new[] { nameClaim, idClaim });
                }
            }

            return result;
        }

        public SearchResult<User> Search(SearchCriteria<User> search)
        {
            return _repository.Search(search);
        }
    }
}
