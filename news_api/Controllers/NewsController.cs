using AutoMapper;
using news_api.Core.Model;
using news_api.DataTransferObjects;
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

namespace news_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public NewsController(IConfiguration configuration, IMapper mapper)
        {
            this._configuration = configuration;
            this._mapper = mapper;
        }        

        [AllowAnonymous]
        [HttpGet("everything", Name = "GetNewsEverything")]
        public async Task<IActionResult> GetNewsEverything([FromQuery]EverythingRequest requestEverything){
            var newsApiClient = new NewsApiClient(_configuration.GetSection("ApiKey").Value);
            var articlesResponse = await newsApiClient.GetEverythingAsync(requestEverything);
            if (articlesResponse.Status == Statuses.Ok)
            {  
                var articlesResponseDto = _mapper.Map<ArticlesResult, ArticlesResultDto>(articlesResponse);
                return Ok(articlesResponseDto);   
            }

            return BadRequest("An error has occured retreiving news");
        }

        [AllowAnonymous]
        [HttpGet("topheadlines", Name = "GetNewsTopheadlines")]
        public async Task<IActionResult> GetNewsTopheadlines([FromQuery] TopHeadlinesRequest topHeadlinesRequest){
            var newsApiClient = new NewsApiClient(_configuration.GetSection("ApiKey").Value);
            var articlesResponse = await newsApiClient.GetTopHeadlinesAsync(topHeadlinesRequest);
            if (articlesResponse.Status == Statuses.Ok)
            {  
                var articlesResponseDto = _mapper.Map<ArticlesResult, ArticlesResultDto>(articlesResponse);
                return Ok(articlesResponseDto);   
            }

            return BadRequest("An error has occured retreiving news");
        }
    }
}