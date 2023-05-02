using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using WebApplication.Services.AuthService;
using WebApplication.Services.UserService;

namespace WebApplication.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static User user = new User();
        private readonly IUserService _userService;
        private readonly Services.AuthService.IAuthorizationService _authService;

        public AuthController(
            IUserService userService,
            Services.AuthService.IAuthorizationService authService)
        {
            _userService = userService;
            _authService = authService;
        }

        [HttpGet, Authorize]
        public IActionResult GetMe()
        {
            var userName = _userService.GetMyName();
            return Ok(userName);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDto request)
        {
            var user = await _userService.CreateUser(request);

            if (!user.IsSuccess)
            {
                return BadRequest(user.ErrorMessage);
            }

            return Ok(user.Data);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserDto request)
        {
            var tresult  = await _authService.Login(request);

            if (!tresult.IsSuccess)
            {
                return Unauthorized(tresult.ErrorMessage);
            }

            return Ok(tresult.Data);
        }

        //[HttpPost("refresh-token")]
        //public async Task<ActionResult<string>> RefreshToken()
        //{
        //    var refreshToken = Request.Cookies["refreshToken"];

        //    if (!user.RefreshToken.Equals(refreshToken))
        //    {
        //        return Unauthorized("Invalid Refresh Token.");
        //    }
        //    else if (user.TokenExpires < DateTime.Now)
        //    {
        //        return Unauthorized("Token expired.");
        //    }

        //    string token = CreateToken(user);
        //    var newRefreshToken = GenerateRefreshToken();
        //    SetRefreshToken(newRefreshToken);

        //    return await Task.FromResult(Ok(token));
        //}
    }
}
