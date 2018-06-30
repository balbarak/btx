using Btx.Client.Domain.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Btx.Server.ViewModels
{
    public class SearchViewModelBase<TModel> : SearchCriteria<TModel> where TModel : class
    {
        public virtual SearchCriteria<TModel> ToSearchModel()
        {
            
            return this;
        }

        public virtual Dictionary<string, string> ToRouteValueDictionary()
        {
            throw new NotImplementedException();
        }

    }
}
