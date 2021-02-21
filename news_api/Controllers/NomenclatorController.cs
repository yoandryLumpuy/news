using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsApiClientClasses.Constants;
using news_api.Extensions;
using System.Linq;
using System.Collections.Generic;

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
            var parcialResult = ExtensionMethods.GetKeyValuePairsFromEnum<Countries>(); 
            var result =  parcialResult
               .Select(elem => new KeyValuePair<int, string>(elem.Key, Constants.GetCountryNameByEnum((Countries)elem.Key)))
               .OrderBy(elem => elem.Value);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("languages", Name = "GetLanguages")]
        public IActionResult GetLanguages(){  
            var parcialResult = ExtensionMethods.GetKeyValuePairsFromEnum<Languages>(); 
            var result =  parcialResult
              .Select(elem => new KeyValuePair<int, string>(elem.Key, Constants.GetLanguageNameByEnum((Languages)elem.Key)))
              .OrderBy(elem => elem.Value);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("sortbys", Name = "GetSortBys")]
        public IActionResult GetSortBys(){            
            return Ok(ExtensionMethods.GetKeyValuePairsFromEnum<SortBys>());
        }

        [AllowAnonymous]
        [HttpGet("categories", Name = "GetCategories")]
        public IActionResult GetCategories(){            
            return Ok(ExtensionMethods.GetKeyValuePairsFromEnum<Categories>());
        }
    }
}