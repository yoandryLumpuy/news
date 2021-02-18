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

        Task<PaginationResult<User>> GetUsers(QueryObject queryObject);   

        Task<PaginationResult<QueryObjectEverythingRequest>> GetTracesEverythingRequests(QueryObject queryObject);   

        Task<PaginationResult<QueryObjectTopHeadLinesRequest>> GetTracesTopheadlinesRequests(QueryObject queryObject);     
    }
}
