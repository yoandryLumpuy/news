using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using news_api.Core.Model;
using news_api.ViewModel;
using news_api.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Reservation_API.Persistence;
using AutoMapper;

namespace news_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public AdminController(UserManager<User> userManager, IRepository repository, IMapper mapper)
        {
            _userManager = userManager;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("roles")]
        [AllowAnonymous]
        public IActionResult GetAvailableRoles()
        {
            return Ok(new List<string>
            {
                Constants.RoleNameNormalUser,
                Constants.RoleNameAdmin
            });
        }

        [HttpPost("editRoles/{userName}")]
        [Authorize(Policy = Constants.PolicyNameAdmin)]
        public async Task<IActionResult> UpdateUserRoles(string userName, EditUserRolesViewModel editUserRolesDto)
        {             
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null) return NotFound("User not found!");

            var roles = await _userManager.GetRolesAsync(user);
            roles = roles ?? new List<string>();

            var result = await _userManager.AddToRolesAsync(user, editUserRolesDto.Roles.Except(roles));
            if (!result.Succeeded) return BadRequest("Failed adding roles to the user.");
            
            result = await _userManager.RemoveFromRolesAsync(user, roles.Except(editUserRolesDto.Roles));
            if (!result.Succeeded) return BadRequest("Failed removing roles from the user");

            return Ok(await _userManager.GetRolesAsync(user));
        }

        [HttpGet("users", Name = "GetUsers")]
        [Authorize(Policy = Constants.PolicyNameAdmin)]
        public async Task<IActionResult> GetUsers([FromQuery] QueryObject queryObject)
        {  
           var usersFromDbContext = await _repository.GetUsersAsync(queryObject);
           return Ok(_mapper.Map<PaginationResult<UserForListViewModel>>(usersFromDbContext));
        }
    }
}