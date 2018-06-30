using System;
using System.Collections.Generic;
using System.Text;

namespace Btx.Client.Domain.Search
{
    public class SearchBase
    {
        public int PageSize { get; set; }

        public int PageNumber { get; set; }

        public SearchBase()
        {
            PageSize = 30;
            PageNumber = 1;
            
        }
    }
}
