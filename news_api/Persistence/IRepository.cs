using System.Collections.Generic;
using System.Threading.Tasks;
using news_api.Core.Model;
using news_api.DataTransferObjects;
using news_api.Extensions;

namespace Reservation_API.Persistence
{
    public interface IRepository
    {
        Task<User> GetUserAsync(int id);
        
    }
}
