using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using news_api.Core.Model;

namespace news_api.EntityConfigurations
{
    public class QueryObjectEverythingRequestConfiguration: IEntityTypeConfiguration<QueryObjectEverythingRequest>
    {     
        public QueryObjectEverythingRequestConfiguration(){}
        public void Configure(EntityTypeBuilder<QueryObjectEverythingRequest> builder)
        {
            builder.HasKey(elem => elem.Id);          

            builder.HasOne(ur => ur.CreatedByUser)
                .WithMany(user => user.EverythingRequests)
                .HasForeignKey(ur => ur.CreatedByUserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);  
        }
    }
}