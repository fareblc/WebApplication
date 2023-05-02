namespace WebApplication.Services.UserService
{
    public interface IUserService
    {
        string GetMyName();

        Task<Result<User>> CreateUser(UserDto user);

        Task<Result<DataAccessLayer.Model.User>> SearchUser(string userName);

        Task<Result<DataAccessLayer.Model.User>> SearchUser(Guid id);
    }
}
