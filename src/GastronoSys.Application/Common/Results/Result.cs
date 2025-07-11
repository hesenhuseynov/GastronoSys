namespace GastronoSys.Application.Common.Results
{
    public class Result
    {
        public bool IsSuccess => Status == ResultStatus.Success || Status == ResultStatus.Created || Status == ResultStatus.Accepted || Status == ResultStatus.NoContent;
        public ResultStatus Status { get; }
        public string? Message { get; }
        public List<string>? Errors { get; }

        protected Result(ResultStatus status, string? message = null, List<string>? errors = null)
        {
            Status = status;
            Message = message;
            Errors = errors;

        }


        public static Result Success(string? message = null) => new(ResultStatus.Success, message);
        public static Result Failure(ResultStatus status, string error) => new(status, error, new List<string> { error });
        public static Result Failure(ResultStatus status, List<string> errors) => new(status, errors?.FirstOrDefault(), errors);

        public static Result Validation(List<string> errors) => new(ResultStatus.ValidationError, "Validation error", errors);

    }

    public class Result<T> : Result
    {
        public T? Value { get; }

        protected Result(T? value, ResultStatus status, string? message = null, List<string>? errors = null)
            : base(status, message, errors)
        {
            Value = value;
        }

        // Helper Methods (full set)
        public static Result<T> Success(T value, string? message = null) => new(value, ResultStatus.Success, message);
        public static Result<T> Created(T value, string? message = null) => new(value, ResultStatus.Created, message);
        public static Result<T> Accepted(T value, string? message = null) => new(value, ResultStatus.Accepted, message);
        public static Result<T> NoContent(string? message = null) => new(default, ResultStatus.NoContent, message);
        public static Result<T> NotFound(string? message = null) => new(default, ResultStatus.NotFound, message ?? "Not found");
        public static Result<T> Conflict(string? message = null) => new(default, ResultStatus.Conflict, message ?? "Conflict occurred");
        public static Result<T> ValidationError(List<string> errors) => new(default, ResultStatus.ValidationError, "Validation error", errors);
        public static Result<T> BadRequest(string? message = null) => new(default, ResultStatus.BadRequest, message ?? "Bad request");
        public static Result<T> Unauthorized(string? message = null) => new(default, ResultStatus.Unauthorized, message ?? "Unauthorized");
        public static Result<T> Forbidden(string? message = null) => new(default, ResultStatus.Forbidden, message ?? "Forbidden");
        public static Result<T> TooManyRequests(string? message = null) => new(default, ResultStatus.TooManyRequests, message ?? "Too many requests");
        public static Result<T> Error(string? message = null) => new(default, ResultStatus.Error, message ?? "An error occurred");

        // Fallback universal method
        public static Result<T> Failure(ResultStatus status, string error) => new(default, status, error, new List<string> { error });
        public static Result<T> Failure(ResultStatus status, List<string> errors) => new(default, status, errors?.FirstOrDefault(), errors);
    }

}
