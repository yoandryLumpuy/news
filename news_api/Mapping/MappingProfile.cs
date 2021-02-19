using System.Linq;
using AutoMapper;
using news_api.Core.Model;
using news_api.ViewModel;
using NewsApiClientClasses.Models;
using news_api.Extensions;
using NewsApiClientClasses.Constants;

namespace Reservation_API.Mapping
{
    public class MappingProfile : Profile
    {        
        public MappingProfile()
        {
            //from View Model to Domain Model 
            CreateMap<UserForLoginViewModel, User>();             
            CreateMap<EverythingRequest, QueryObjectEverythingRequest>()                
                .ForMember(target => target.Sources, 
                    opt => opt.MapFrom(source => source.Sources == null || source.Sources.Count == 0 
                                                 ? string.Empty 
                                                 : string.Join(',', source.Sources)))
                .ForMember(target => target.Domains, 
                    opt => opt.MapFrom(source => source.Domains == null || source.Domains.Count == 0 
                                                 ? string.Empty 
                                                 : string.Join(',', source.Domains)))
                .ForMember(target => target.Language, 
                        opt => opt.MapFrom(source => !source.Language.HasValue ? string.Empty : source.Language.ToString()))
                .ForMember(target => target.SortBy,  
                        opt => opt.MapFrom(source => !source.SortBy.HasValue ? string.Empty : source.SortBy.ToString()));

            CreateMap<TopHeadlinesRequest, QueryObjectTopHeadLinesRequest>()
                .ForMember(target => target.Sources, opt => opt.Ignore())
                .ForMember(target => target.Sources, 
                    opt => opt.MapFrom(source => source.Sources == null || source.Sources.Count == 0 
                                                 ? string.Empty 
                                                 : string.Join(',', source.Sources)))
                .ForMember(target => target.Language, 
                        opt => opt.MapFrom(source => !source.Language.HasValue ? string.Empty : source.Language.ToString()))
                .ForMember(target => target.Category, 
                        opt => opt.MapFrom(source => !source.Category.HasValue ? string.Empty : source.Category.ToString()))
                .ForMember(target => target.Country, 
                        opt => opt.MapFrom(source => !source.Country.HasValue ? string.Empty : source.Country.ToString()));


            //from Domain Model to View Model    
            CreateMap<User, UserForListViewModel>()
                .ForMember(userDto => userDto.Roles, 
                    memberOptions => memberOptions.MapFrom(user => user.UserRoles.Select(userRole => userRole.Role.Name).ToArray()));   
            
            CreateMap<Source, ArticleSourceViewModel>();
            CreateMap<Article, ArticleViewModel>();
            CreateMap<ArticlesResult, ArticlesResultViewModel>()
                .ForMember(articleResultDto => articleResultDto.Error, 
                    opt => opt.MapFrom(articleResult => articleResult.Error != null && articleResult.Error.Message != null 
                                                        ? articleResult.Error.Message 
                                                        : string.Empty))
                .ForMember(articleResultDto => articleResultDto.Status, 
                    opt => opt.MapFrom(articleResult => 
                        ExtensionMethods.GetEnumStringValues<Statuses>().FirstOrDefault(elem => (Statuses)elem.Key == articleResult.Status).Value));

            CreateMap<QueryObjectEverythingRequest, QueryObjectEverythingRequestViewModel>()
                .ForMember(target => target.CreatedByUser, opt => opt.MapFrom(source => source.CreatedByUser.UserName));

            CreateMap<QueryObjectTopHeadLinesRequest, QueryObjectTopHeadLinesRequestViewModel>()
                .ForMember(target => target.CreatedByUser, opt => opt.MapFrom(source => source.CreatedByUser.UserName));

            CreateMap(typeof(PaginationResult<>), typeof(PaginationResult<>));              
        }
    }
}
