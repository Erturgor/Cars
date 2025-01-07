using Cars.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using System.Data;
using System.Security.Claims;

namespace Cars.API.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController: ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly TokenService tokenService;
        public AccountController(UserManager<AppUser> userManager, TokenService token)
        {
            _userManager = userManager;
            tokenService = token;
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);
            if (user == null) return Unauthorized();
            var result = await _userManager.CheckPasswordAsync(user, loginDTO.Password);
            if (result)
            {
                return new UserDTO
                {
                    DisplayName = user.DisplayName,
                    UserName = user.UserName,
                    Role = _userManager.GetRolesAsync(user).Result.ToList(),
                    Token = tokenService.CreateToken(user)
                };
            }
            return Unauthorized();
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO)
        {
            if(await _userManager.Users.AnyAsync(x => x.UserName ==registerDTO.UserName))
            {
                return BadRequest("Username is taken.");
            }
            var user = new AppUser
            {
                DisplayName = registerDTO.DisplayName,
                Email = registerDTO.Email,
                UserName = registerDTO.UserName,
            };
            var result = await _userManager.CreateAsync(user, registerDTO.Password);
            var result2 = await _userManager.AddToRoleAsync(user, "User");
            if (result.Succeeded)
            {
                return new UserDTO
                {
                    DisplayName = user.DisplayName,
                    UserName = user.UserName,
                    Role = new List<string> { "User" },
                    Token = tokenService.CreateToken(user),
                };
            }
            return BadRequest("Problem with registering user");
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDTO>> GetCurrentUser()
        {
            var user = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));
            return new UserDTO
            {
                DisplayName = user.DisplayName,

                Token = tokenService.CreateToken(user),
                UserName = user.UserName
            };
        }
    }
}
