namespace GymApp.BLL.Common
{
    public class Result
    {
        protected Result(bool isSuccess, string? error = null, ResultErrorType? errorType = null)
        {
            IsSuccess = isSuccess;
            Error = error;
            ErrorType = errorType;
        }
        public bool IsSuccess { get; set; } = false;
        public string? Error { get; set; }
        public ResultErrorType? ErrorType { get; set; }

        public static Result Ok() => new(true, null ,null);
        public static Result ValidationError(string error = "Validation Error") => new(false, error, ResultErrorType.ValidationError);
        public static Result NotFound(string error = "Not Found") => new(false, error, ResultErrorType.NotFound);
    }

    public class Result<TResult> : Result
    {
        protected Result(bool isSuccess, string? error, ResultErrorType? errorType) : base(isSuccess, error, errorType)
        {
        }
        public TResult? Data { get; set; }
        public static Result<TResult> Ok(TResult data) => new(true, null, null) { Data = data };
        public static Result<TResult> ValidationError(string error = "Validation Error") => new Result<TResult>(false, error, ResultErrorType.ValidationError);
        public static Result<TResult> NotFound(string error = "Not Found") => new Result<TResult>(false, error, ResultErrorType.NotFound);
    }
    public enum ResultErrorType
    {
        NotFound = 1,
        ValidationError = 2,
        Unauthorized = 3,
        Forbidden = 4,
        Conflict = 5,
        InternalServerError = 6
       
    }
}
