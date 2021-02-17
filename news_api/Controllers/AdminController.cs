using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using news_api.Core.Model;
using news_api.DataTransferObjects;
using news_api.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace news_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly UserManager<User> _userManager;

        public AdminController(UserManager<User> userManager)
        {
            _userManager = userManager;
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
        public async Task<IActionResult> UpdateUserRoles(string userName, EditUserRolesDto editUserRolesDto)
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
    }
}