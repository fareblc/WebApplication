namespace WebApplication.Services.AuthService
{
    public interface IAuthorizationService
    {
        Task<Result<string>> Register(UserDto user);

        Task<Result<string>> Login(UserDto user);
    }
}
