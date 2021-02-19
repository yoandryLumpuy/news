using System;
using System.Collections.Generic;
using NewsApiClientClasses.Models;

namespace news_api.Core.Model
{
    public class QueryObjectEverythingRequest 
    {
        public int Id { get; set; }
        public User CreatedByUser { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Q { get; set; }        
        public string Sources {get; set;}       
        public string Domains {get; set;}         
        public DateTime? From { get; set; }        
        public DateTime? To { get; set; }
        public string Language { get; set; }        
        public string SortBy { get; set; }       
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}