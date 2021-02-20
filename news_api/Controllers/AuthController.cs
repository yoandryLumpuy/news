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
using Reservation_API.Persistence;

namespace news_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly IRepository _repository;

        public AuthController(IConfiguration config, UserManager<User> userManager,
                              RoleManager<Role> roleManager,
                              SignInManager<User> signInManager, IMapper mapper,
                              IRepository repository)
        {
            _config = config;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _repository = repository;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]UserForLoginViewModel userForLoginDto)
        {
            var user = await _userManager.FindByNameAsync(userForLoginDto.UserName);

            if (user == null)
                return BadRequest("The user doesn't exist !");

            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, userForLoginDto.Password, false);

            if (!signInResult.Succeeded) return Unauthorized("Wrong password!. Try again.");

            user = await _repository.GetUserAsync(user.Id);
            var userToListDto = _mapper.Map<UserForListViewModel>(user);
            return Ok(new
            {
                Token = await GenerateTokenAsync(user),
                User = userToListDto
            });

        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody]UserForLoginViewModel userForLoginDto)
        {
            var userApp = _mapper.Map<User>(userForLoginDto);
            var identityResult = await _userManager.CreateAsync(userApp, userForLoginDto.Password);

            if (identityResult.Succeeded)
            {
                await _userManager.AddToRoleAsync(userApp, Constants.RoleNameNormalUser);                                
                var userToReturn = _mapper.Map<UserForListViewModel>(userApp);
                return Ok(userToReturn);
            }

            return BadRequest(identityResult.Errors);
        }

        public async Task<string> GenerateTokenAsync(User user)
        {
            var userRoles =  await _userManager.GetRolesAsync(user);
            var securityTokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName) 
                }.Concat(userRoles.Select(role => new Claim(ClaimTypes.Role, role)))),

                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(
                    key:new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config.GetSection("AppSettings:SecurityKey").Value)), 
                    algorithm: SecurityAlgorithms.HmacSha512Signature)

            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(securityTokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}