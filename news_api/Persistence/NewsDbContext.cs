using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using news_api.Core.Model;
using news_api.EntityConfigurations;

namespace news_api.Persistence
{
    public class NewsDbContext : IdentityDbContext<User, Role,int, IdentityUserClaim<int>,UserRole,IdentityUserLogin<int>,IdentityRoleClaim<int>,IdentityUserToken<int>>
    {       
        public DbSet<QueryObjectEverythingRequest> EverythingRequests { get; set; }   
        public DbSet<QueryObjectTopHeadLinesRequest> TopHeadLinesRequests { get; set; } 
        public NewsDbContext(DbContextOptions<NewsDbContext> options) : base(options)
        { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);                        
            builder.ApplyConfiguration(new UserRoleConfiguration());
        }
    }
}
