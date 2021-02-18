using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Reservation_API.Persistence;
using Microsoft.AspNetCore.Authorization;
using news_api.Extensions;
using news_api.Core.Model;
using news_api.ViewModel;
using System.Threading.Tasks;

namespace news_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueriesTracerController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRepository _repository;

        public QueriesTracerController(IMapper mapper, IRepository repository)
        {            
            this._mapper = mapper;
            this._repository = repository;
        } 
        
        [HttpGet("everythingRequest", Name = "GetEverythingRequest")]
        [Authorize(Policy = Constants.PolicyNameAdmin)]
        public async Task<IActionResult> GetEverythingRequest(QueryObject queryObject){
            var paginatedResultsFromDb = await _repository.GetTracesEverythingRequests(queryObject);
            var mappedResults 
                = _mapper.Map<PaginationResult<QueryObjectEverythingRequest>,PaginationResult<QueryObjectEverythingRequestViewModel>>(paginatedResultsFromDb);
            return Ok(mappedResults);
        }

        [HttpGet("topheadlinesRequest", Name = "GetTopheadlinesRequest")]
        [Authorize(Policy = Constants.PolicyNameAdmin)]
        public async Task<IActionResult> GetTopheadlinesRequest(QueryObject queryObject){
            var paginatedResultsFromDb = await _repository.GetTracesTopheadlinesRequests(queryObject);
            var mappedResults 
                = _mapper.Map<PaginationResult<QueryObjectTopHeadLinesRequest>,PaginationResult<QueryObjectTopHeadLinesRequestViewModel>>(paginatedResultsFromDb);
            return Ok(mappedResults);
        }
    }
}