using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Btx.Client.Domain.Models;
using Btx.Client.Domain.Search;
using Btx.Server.Domain;
using Btx.Server.Identity;
using Btx.Server.Services;
using Btx.Server.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Btx.Server.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BtxUserController : BaseApiController
    {
        private ILogger _logger;

        public BtxUserController(ILogger<LoginController> logger, IConfiguration config, BtxUserManager userManager): base(config,userManager)
        {
            _logger = logger;
        }

        public IActionResult Post([FromBody]BtxUserSearch model)
        {
            var search = new UserSearchViewModel()
            {
                Username = model.Username,
                PageNumber = model.PageNumber,
                PageSize = model.PageSize
            };


            var searchResult = UserService.Instance.Search(search.ToSearchModel());

            var result = new SearchResult<BtxUser>()
            {
                PageNumber = searchResult.PageNumber,
                PageSize = searchResult.PageSize,
                TotalResultsCount = searchResult.TotalResultsCount
            };

            result.Result = searchResult.Result.Select(a => a.ToBtxUser()).ToList();
           
            return Ok(result);
        }
    }
}