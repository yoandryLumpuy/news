using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace news_api.Core.Model
{
    public class Role : IdentityRole<int>
    {
        public ICollection<UserRole> UserRoles{ get; set; }

        public Role(){
            UserRoles = new List<UserRole>();
        }
    }
}
