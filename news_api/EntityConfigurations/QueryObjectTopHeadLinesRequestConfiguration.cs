using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using news_api.Core.Model;

namespace news_api.EntityConfigurations
{
    public class QueryObjectTopHeadLinesRequestConfiguration: IEntityTypeConfiguration<QueryObjectTopHeadLinesRequest>
    {     
        public QueryObjectTopHeadLinesRequestConfiguration(){}
        public void Configure(EntityTypeBuilder<QueryObjectTopHeadLinesRequest> builder)
        {            
            builder.HasOne(ur => ur.CreatedByUser)
                .WithMany(user => user.TopHeadLinesRequests)
                .HasForeignKey(ur => ur.CreatedByUserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);  

            builder.Property(queryObject => queryObject.Sources) 
            .HasMaxLength(500);           
        }
    }
}