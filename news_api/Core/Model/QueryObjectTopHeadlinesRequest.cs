using System;
using System.Collections.Generic;
using NewsApiClientClasses.Models;

namespace news_api.Core.Model
{
    public class QueryObjectTopHeadLinesRequest 
    {
        public int Id { get; set; }
        public User CreatedByUser { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedAt { get; set; }        
        public string Q { get; set; }
        public string Sources {get; set;}
        public string Category { get; set; }
        public string Language { get; set; }
        public string Country { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}