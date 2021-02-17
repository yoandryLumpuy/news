using System.Threading.Tasks;

namespace news_api.Core
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}
