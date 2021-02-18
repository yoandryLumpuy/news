using AutoMapper;
using news_api.Core.Model;
using news_api.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using news_api.Extensions;
using NewsApiClientClasses;
using NewsApiClientClasses.Models;
using NewsApiClientClasses.Constants;
using System.Collections.Generic;
using Reservation_API.Persistence;

namespace news_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IRepository _repository;

        public NewsController(IConfiguration configuration, IMapper mapper, IRepository repository)
        {
            this._configuration = configuration;
            this._mapper = mapper;
            this._repository = repository;
        }        

        [Authorize(Policy = Constants.PolicyNameNormalAccess)]
        [HttpGet("everything", Name = "GetNewsEverything")]
        public async Task<IActionResult> GetNewsEverything([FromQuery]EverythingRequest requestEverything){
            var invokingUser = int.Parse(User.FindFirst(claim => claim.Type == ClaimTypes.NameIdentifier).Value);

            var newsApiClient = new NewsApiClient(_configuration.GetSection("ApiKey").Value);
            var articlesResponse = await newsApiClient.GetEverythingAsync(requestEverything);
            if (articlesResponse.Status == Statuses.Ok)
            {  
                var queryObject = _mapper.Map<EverythingRequest, QueryObjectEverythingRequest>(requestEverything);
                await _repository.PostQueryObjectEverythingRequestAsync(invokingUser, queryObject);

                var articlesResponseDto = _mapper.Map<ArticlesResult, ArticlesResultViewModel>(articlesResponse);
                return Ok(articlesResponseDto);   
            }

            return BadRequest("An error has occured retreiving news");
        }

        [Authorize(Policy = Constants.PolicyNameNormalAccess)]
        [HttpGet("topheadlines", Name = "GetNewsTopheadlines")]
        public async Task<IActionResult> GetNewsTopheadlines([FromQuery] TopHeadlinesRequest topHeadlinesRequest){
            var invokingUser = int.Parse(User.FindFirst(claim => claim.Type == ClaimTypes.NameIdentifier).Value);

            var newsApiClient = new NewsApiClient(_configuration.GetSection("ApiKey").Value);
            var articlesResponse = await newsApiClient.GetTopHeadlinesAsync(topHeadlinesRequest);
            if (articlesResponse.Status == Statuses.Ok)
            {  
                var queryObject = _mapper.Map<TopHeadlinesRequest, QueryObjectTopHeadLinesRequest>(topHeadlinesRequest);
                await _repository.PostQueryObjectTopHeadLinesRequestAsync(invokingUser, queryObject);

                var articlesResponseDto = _mapper.Map<ArticlesResult, ArticlesResultViewModel>(articlesResponse);
                return Ok(articlesResponseDto);   
            }

            return BadRequest("An error has occured retreiving news");
        }
    }
}