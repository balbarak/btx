using Btx.Client.Domain.Search;
using Btx.Server.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Btx.Server.ViewModels
{
    public class UserSearchViewModel : SearchViewModelBase<User>
    {
        public string Username { get; set; }

        public string Nickname { get; set; }

        public override SearchCriteria<User> ToSearchModel()
        {
            if (!string.IsNullOrWhiteSpace(Username))
                AddAndFilter(a => a.UserName.Contains(Username));
            
            return this;
        }
    }
}
