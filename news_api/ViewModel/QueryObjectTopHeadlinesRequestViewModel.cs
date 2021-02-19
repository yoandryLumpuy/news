using System;
using System.Collections.Generic;

namespace news_api.ViewModel
{
    public class QueryObjectTopHeadLinesRequestViewModel
    {
        public int Id { get; set; }
        public string CreatedByUser { get; set; }
        public DateTime CreatedAt { get; set; }       
        public string Q { get; set; }       
        public string Sources;        
        public string Category { get; set; }       
        public string Language { get; set; }       
        public string Country { get; set; }        
        public int Page { get; set; }      
        public int PageSize { get; set; }
    }
}