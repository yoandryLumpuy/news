
using Microsoft.AspNetCore.Identity;

namespace news_api.Core.Model
{
    public class UserRole : IdentityUserRole<int>
    {
        public User User { get; set; }
        public Role Role { get; set; }
    }
}
