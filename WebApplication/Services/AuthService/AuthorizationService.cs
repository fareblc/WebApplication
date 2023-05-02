using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WebApplication.Services.UserService;

namespace WebApplication.Services.AuthService
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public AuthorizationService(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;

        }

        public async Task<Result<string>> Login(UserDto user)
        {
            var uresult = await _userService.SearchUser(user.Username);

            if (!uresult.IsSuccess)
            {
                return Result<string>.CreateFailed(uresult.ErrorMessage);
            }

            var hash = Convert.FromBase64String(uresult.Data.PasswordHash);
            var salt = Convert.FromBase64String(uresult.Data.PasswordSalt);

            if(!VerifyPasswordHash(user.Username, hash, salt))
            {
                return Result<string>.CreateFailed("Wrong Password");
            }

            string token = CreateToken(uresult.Data);

            var refreshToken = GenerateRefreshToken();

            return Result<string>.CreateSuccessful(token);
        }

        public async Task<Result<string>> Register(UserDto user)
        {
            var uresult = await _userService.CreateUser(user);

            if (!uresult.IsSuccess)
            {
                return Result<string>.CreateFailed(uresult.ErrorMessage);
            }

            return Result<string>.CreateSuccessful(uresult.Data.Username);
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private string CreateToken(DataAccessLayer.Model.User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddDays(7),
                Created = DateTime.Now
            };

            return refreshToken;
        }

        //private void SetRefreshToken(RefreshToken newRefreshToken)
        //{
        //    var cookieOptions = new CookieOptions
        //    {
        //        HttpOnly = true,
        //        Expires = newRefreshToken.Expires
        //    };
        //    Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);

        //    user.RefreshToken = newRefreshToken.Token;
        //    user.TokenCreated = newRefreshToken.Created;
        //    user.TokenExpires = newRefreshToken.Expires;
        //}
    }
}
