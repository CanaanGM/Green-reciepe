using Azure.Core;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Recipie_Backend.Data.Models;
using Recipie_Backend.Services;
using System.Security.Claims;
using Recipie_Backend.Data.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Recipie_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly TokenService _token;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, TokenService token)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _token = token;
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(AppLoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null) return Unauthorized();

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (result.Succeeded)
            {
                await SetRefreshToken(user);
                return CreateUserObject(user);
            }
            return Unauthorized();
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(AppRegisterDto registerDto)
        {
            if (await _userManager.Users.AnyAsync(x => x.Email == registerDto.Email)) return BadRequest("Email Taken!");
            if (await _userManager.Users.AnyAsync(x => x.DisplayName == registerDto.DisplayName)) return BadRequest("Display Name Taken!");
            if (await _userManager.Users.AnyAsync(x => x.UserName == registerDto.UserName)) return BadRequest("User Name Taken!");

            var user = new User
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.UserName
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (result.Succeeded)
            {
                await SetRefreshToken(user);
                return CreateUserObject(user);
            }
            return BadRequest("Problem registering user . . . ");
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var user = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));
            await SetRefreshToken(user);
            return CreateUserObject(user);
        }

        private UserDto CreateUserObject(User user) => new UserDto
        {
            DisplayName = user.DisplayName,
            UserName = user.UserName,
            Token = _token.CreateToken(user)
        };

        [Authorize]
        [HttpPost("refreshToken")]
        public async Task<ActionResult<UserDto>> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var user = await _userManager.Users
                .Include(r => r.RefreshTokens)
                .FirstOrDefaultAsync(x => x.UserName == User.FindFirstValue(ClaimTypes.Name));

            if (user == null) return Unauthorized();

            var oldToken = user.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken);
            if (oldToken != null && !oldToken.IsActive) return Unauthorized();
            return CreateUserObject(user);

        }

        private async Task SetRefreshToken(User User)
        {
            var refreshToken = _token.GenerateRefreshToken();

            User.RefreshTokens.Add(refreshToken);
            await _userManager.UpdateAsync(User);

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTimeOffset.UtcNow.AddDays(7),
            };

            Response.Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);
        }


    }
}
