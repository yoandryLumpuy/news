using System;
using NewsApiClientClasses.Models;

namespace news_api.Core.Model
{
    public class QueryObjectTopHeadLinesRequest : TopHeadlinesRequest
    {
        public int Id { get; set; }
        public User CreatedByUser { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedAt { get; set; }

        public new string Sources { get; set; }
    }
}