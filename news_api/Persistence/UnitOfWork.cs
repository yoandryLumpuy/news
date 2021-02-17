using System.Threading.Tasks;
using news_api.Core;

namespace news_api.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly NewsDbContext context;

        public UnitOfWork(NewsDbContext context)
        {
            this.context = context;
        }

        public async Task CompleteAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
