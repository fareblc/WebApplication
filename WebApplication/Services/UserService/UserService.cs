using System.Text;
using System.Security.Claims;
using WebApplication.DataAccessLayer;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly WebApplicationContext _dbContext;

        public UserService(IHttpContextAccessor httpContextAccessor,
            WebApplicationContext dbContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _dbContext = dbContext;
        }

        public async Task<Result<User>> CreateUser(UserDto user)
        {
            CreatePasswordHash(user.Password, out var hash, out var salt);

            var userModel = new DataAccessLayer.Model.User
            {
                Username = user.Username,
                PasswordHash = Convert.ToBase64String(hash),
                PasswordSalt = Convert.ToBase64String(salt)
            };

            _dbContext.Add(userModel);
            await _dbContext.SaveChangesAsync();

            var newUser = new User
            {
                Username = userModel.Username
            };

            return Result<User>.CreateSuccessful(newUser);
        }

        public string GetMyName()
        {
            var result = string.Empty;
            if (_httpContextAccessor.HttpContext != null)
            {
                result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            }
            return result;
        }

        public async Task<Result<DataAccessLayer.Model.User>> SearchUser(string userName)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Username.Equals(userName));

            if (user == null)
            {
                return Result<DataAccessLayer.Model.User>.CreateFailed("user not found");
            }

            return Result<DataAccessLayer.Model.User>.CreateSuccessful(user);
        }

        public async Task<Result<DataAccessLayer.Model.User>> SearchUser(Guid id)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return Result<DataAccessLayer.Model.User>.CreateFailed("user not found");
            }

            return Result<DataAccessLayer.Model.User>.CreateSuccessful(user);
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
