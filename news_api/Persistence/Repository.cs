
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using news_api.Core.Model;
using news_api.ViewModel;
using news_api.Core;
using AutoMapper;
using news_api.Extensions;
using news_api.Persistence;

namespace Reservation_API.Persistence
{
    public class Repository : IRepository
    {
        private readonly NewsDbContext _newsDbContext;
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public Repository(NewsDbContext newsDbContext, UserManager<User> userManager,
                  IUnitOfWork unitOfWork)
        {
            _newsDbContext = newsDbContext;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        public async Task<QueryObjectEverythingRequest> GetQueryObjectEverythingRequestAsync(int id)
        {
            return await  _newsDbContext.EverythingRequests.EagerLoadRelatedObjects()
                           .FirstOrDefaultAsync(elem => elem.Id == id);
        }

        public async Task<QueryObjectTopHeadLinesRequest> GetQueryObjectTopHeadLinesRequestAsync(int id)
        {
            return await  _newsDbContext.TopHeadLinesRequests.EagerLoadRelatedObjects()
                           .FirstOrDefaultAsync(elem => elem.Id == id);
        }

        public async Task<PaginationResult<QueryObjectEverythingRequest>> GetTracesEverythingRequestsAsync(QueryObject queryObject)
        {
            var query = _newsDbContext.EverythingRequests.EagerLoadRelatedObjects().AsQueryable();

            var dictionary = new Dictionary<string, Expression<Func<QueryObjectEverythingRequest, object>>>(){               
                [Constants.SortByCreatedByUser] = request => request.CreatedByUser.UserName,
                [Constants.SortByQ] = request => request.Q,
                [Constants.SortByCreatedAt] = request => request.CreatedAt,
                [Constants.SortByLanguage] = request => request.Language,
                [Constants.SortByFromDatetime] = request => request.From,
                [Constants.SortByToDatetime] = request => request.To               
            };

            if (queryObject?.SortBy != null && dictionary.ContainsKey(queryObject.SortBy))
            {
                query = queryObject.IsSortAscending                 
                        ? query.OrderBy(dictionary[queryObject.SortBy]) 
                        : query.OrderByDescending(dictionary[queryObject.SortBy]);            
            }     

            return await PaginationResult<QueryObjectEverythingRequest>.CreateAsync(query, queryObject.Page, queryObject.PageSize);
        }

        public async Task<PaginationResult<QueryObjectTopHeadLinesRequest>> GetTracesTopheadlinesRequestsAsync(QueryObject queryObject)
        {
            var query = _newsDbContext.TopHeadLinesRequests.EagerLoadRelatedObjects().AsQueryable();

            var dictionary = new Dictionary<string, Expression<Func<QueryObjectTopHeadLinesRequest, object>>>(){               
                [Constants.SortByCreatedByUser] = request => request.CreatedByUser.UserName,
                [Constants.SortByQ] = request => request.Q,
                [Constants.SortByCreatedAt] = request => request.CreatedAt,
                [Constants.SortByLanguage] = request => request.Language,
                [Constants.SortByCategory] = request => request.Category,
                [Constants.SortByCountry] = request => request.Country               
            };

            if (queryObject?.SortBy != null && dictionary.ContainsKey(queryObject.SortBy))
            {
                query = queryObject.IsSortAscending                 
                        ? query.OrderBy(dictionary[queryObject.SortBy]) 
                        : query.OrderByDescending(dictionary[queryObject.SortBy]);            
            }     

            return await PaginationResult<QueryObjectTopHeadLinesRequest>.CreateAsync(query, queryObject.Page, queryObject.PageSize);
        }

        public async Task<User> GetUserAsync(int id)
        {
            return await _userManager.Users
                     .Include(u => u.UserRoles).ThenInclude(ur => ur.Role)
                     .SingleOrDefaultAsync(u => u.Id == id);
        }

        public async Task<PaginationResult<User>> GetUsersAsync(QueryObject queryObject)
        {
            var query = _newsDbContext.Users.Include(u => u.UserRoles).ThenInclude(ur => ur.Role).AsQueryable();            
            return await PaginationResult<User>.CreateAsync(query, queryObject.Page, queryObject.PageSize);
        }

        public async Task<QueryObjectEverythingRequest> PostQueryObjectEverythingRequestAsync(int invokingUser, 
                QueryObjectEverythingRequest queryObjectEverythingRequest)
        {
            queryObjectEverythingRequest.CreatedByUserId = invokingUser;
            queryObjectEverythingRequest.CreatedAt = DateTime.Now;

            await _newsDbContext.EverythingRequests.AddAsync(queryObjectEverythingRequest);
            await _unitOfWork.CompleteAsync();
            return await GetQueryObjectEverythingRequestAsync(queryObjectEverythingRequest.Id);
        }

        public async  Task<QueryObjectTopHeadLinesRequest> PostQueryObjectTopHeadLinesRequestAsync(int invokingUser, 
                QueryObjectTopHeadLinesRequest queryObjectTopHeadLinesRequest)
        {
            queryObjectTopHeadLinesRequest.CreatedByUserId = invokingUser;
            queryObjectTopHeadLinesRequest.CreatedAt = DateTime.Now;

            await _newsDbContext.TopHeadLinesRequests.AddAsync(queryObjectTopHeadLinesRequest);
            await _unitOfWork.CompleteAsync();
            return await GetQueryObjectTopHeadLinesRequestAsync(queryObjectTopHeadLinesRequest.Id);
        }
    }
}
