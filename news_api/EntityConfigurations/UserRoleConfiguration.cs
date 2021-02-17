using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using news_api.Core.Model;

namespace news_api.EntityConfigurations
{
    public class UserRoleConfiguration: IEntityTypeConfiguration<UserRole>
    {     
        public UserRoleConfiguration(){}
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasKey(ur => new {ur.UserId, ur.RoleId});
            
            // userRole (* <-> 1) User
            builder.HasOne(ur => ur.User)
                .WithMany(user => user.UserRoles)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            // userRole (* <-> 1) Role
            builder.HasOne(ur => ur.Role)
                .WithMany(role => role.UserRoles)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }    
}