using System;
using System.Collections.Generic;

namespace news_api.ViewModel
{
    public class QueryObjectEverythingRequestViewModel
    {
        public int Id { get; set; }
        public string CreatedByUser { get; set; }
        public DateTime CreatedAt { get; set; }

        public string Q { get; set; }
       
        public string Sources;
       
        public string Domains;
      
        public DateTime? From { get; set; }
       
        public DateTime? To { get; set; }
      
        public string Language { get; set; }
       
        public string SortBy { get; set; }
       
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}