namespace WebApplication.Services
{
    public class Result<T>
    {
        public bool IsSuccess { get; }
        public string ErrorMessage { get; }
        public T Data { get; }

        private Result(bool isSuccess, string errorMessage, T data)
        {
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
            Data = data;
        }

        public static Result<T> CreateFailed(string errorMessage)
        {
            return new Result<T>(false, errorMessage, default);
        }

        public static Result<T> CreateSuccessful(T data)
        {
            return new Result<T>(true, string.Empty, data);
        }
    }
}
