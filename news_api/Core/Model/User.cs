using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using NewsApiClientClasses.Constants;

namespace news_api.Core.Model
{
    public class User : IdentityUser<int>
    {
        public Countries Country { get; set; }
        public Languages Language { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<QueryObjectTopHeadLinesRequest> TopHeadLinesRequests { get; set; }
        public ICollection<QueryObjectEverythingRequest> EverythingRequests { get; set; }
        public User(){
            UserRoles = new List<UserRole>();
            TopHeadLinesRequests = new List<QueryObjectTopHeadLinesRequest>();
            EverythingRequests = new List<QueryObjectEverythingRequest>();
        }        
    }
}
