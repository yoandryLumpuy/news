using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsApiClientClasses.Constants;
using news_api.Extensions;

namespace news_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NomenclatorController : ControllerBase
    {
        public NomenclatorController(){}

        [AllowAnonymous]
        [HttpGet("countries", Name = "GetCountries")]
        public IActionResult GetCountries(){            
            return Ok(ExtensionMethods.GetEnumStringValues<Countries>());
        }

        [AllowAnonymous]
        [HttpGet("languages", Name = "GetLanguages")]
        public IActionResult GetLanguages(){            
            return Ok(ExtensionMethods.GetEnumStringValues<Languages>());
        }

        [AllowAnonymous]
        [HttpGet("sortbys", Name = "GetSortBys")]
        public IActionResult GetSortBys(){            
            return Ok(ExtensionMethods.GetEnumStringValues<SortBys>());
        }

        [AllowAnonymous]
        [HttpGet("categories", Name = "GetCategories")]
        public IActionResult GetCategories(){            
            return Ok(ExtensionMethods.GetEnumStringValues<Categories>());
        }
    }
}