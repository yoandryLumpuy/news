using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace news_api.Core.Model
{
    public class User : IdentityUser<int>
    {
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
