using System.Collections.Generic;
using System.Threading.Tasks;
using news_api.Core.Model;
using news_api.ViewModel;
using news_api.Extensions;

namespace Reservation_API.Persistence
{
    public interface IRepository
    {
        Task<User> GetUserAsync(int id);
        Task<QueryObjectEverythingRequest> PostQueryObjectEverythingRequestAsync(int invokingUser, 
                QueryObjectEverythingRequest queryObjectEverythingRequest);
        Task<QueryObjectTopHeadLinesRequest> PostQueryObjectTopHeadLinesRequestAsync(int invokingUser, 
                QueryObjectTopHeadLinesRequest queryObjectTopHeadLinesRequest); 

        Task<QueryObjectEverythingRequest> GetQueryObjectEverythingRequestAsync(int id);
        Task<QueryObjectTopHeadLinesRequest> GetQueryObjectTopHeadLinesRequestAsync(int id);   

        Task<PaginationResult<User>> GetUsersAsync(QueryObject queryObject);   

        Task<PaginationResult<QueryObjectEverythingRequest>> GetTracesEverythingRequestsAsync(QueryObject queryObject);   

        Task<PaginationResult<QueryObjectTopHeadLinesRequest>> GetTracesTopheadlinesRequestsAsync(QueryObject queryObject);     
    }
}
