using System.Linq;
using AutoMapper;
using news_api.Core.Model;
using news_api.DataTransferObjects;
using NewsApiClientClasses.Models;
using news_api.Extensions;
using NewsApiClientClasses.Constants;

namespace Reservation_API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //from Dtos to Domain Model 
            CreateMap<UserForLoginDto, User>();             
            CreateMap<EverythingRequest, QueryObjectEverythingRequest>();
            CreateMap<TopHeadlinesRequest, QueryObjectTopHeadLinesRequest>();
            CreateMap<Source, ArticleSourceDto>();
            CreateMap<Article, ArticleDto>();
            CreateMap<ArticlesResult, ArticlesResultDto>()
                .ForMember(articleResultDto => articleResultDto.Error, 
                    opt => opt.MapFrom(articleResult => articleResult.Error != null && articleResult.Error.Message != null 
                                                        ? articleResult.Error.Message 
                                                        : string.Empty))
                .ForMember(articleResultDto => articleResultDto.Status, 
                    opt => opt.MapFrom(articleResult => 
                        ExtensionMethods.GetEnumStringValues<Statuses>().FirstOrDefault(elem => (Statuses)elem.Key == articleResult.Status).Value));


            //from Domain Model to Dtos     
            CreateMap<User, UserForListDto>()
                .ForMember(userDto => userDto.Roles, 
                    memberOptions => memberOptions.MapFrom(user => user.UserRoles.Select(userRole => userRole.Role.Name).ToArray()));                        
            CreateMap(typeof(PaginationResult<>), typeof(PaginationResult<>));              
        }
    }
}
