using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using news_api.Core;

namespace news_api.ViewModel
{
    public class QueryObject : IQueryObject
    {
        //sorting
        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }

        //pagination
        private byte _pageSize = 10;
        private const byte MaxPageSize = 50;
        public int Page { get; set; }
        public byte PageSize
        {
            get => _pageSize;
            set { _pageSize = value > MaxPageSize ? MaxPageSize : value; }
        }
    }
}
