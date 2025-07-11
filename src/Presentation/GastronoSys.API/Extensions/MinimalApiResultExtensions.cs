using GastronoSys.Application.Common.Results;

namespace GastronoSys.API.Extensions
{
    public static class MinimalApiResultExtensions
    {
        public static IResult ToMinimalApiResult<T>(this Result<T> result)
        {
            return result.Status switch
            {
                ResultStatus.Success => Results.Ok(result.Value),
                ResultStatus.Created => Results.Created(string.Empty, result.Value),
                ResultStatus.Accepted => Results.Accepted(string.Empty, result.Value),
                ResultStatus.NoContent => Results.NoContent(),
                ResultStatus.NotFound => Results.NotFound(result.Message ?? result.Errors?.FirstOrDefault()),
                ResultStatus.ValidationError => Results.UnprocessableEntity(result.Errors),
                ResultStatus.BadRequest => Results.BadRequest(result.Message ?? result.Errors?.FirstOrDefault()),
                ResultStatus.Conflict => Results.Conflict(result.Message ?? result.Errors?.FirstOrDefault()),
                ResultStatus.Unauthorized => Results.Unauthorized(),
                ResultStatus.Forbidden => Results.Forbid(),
                ResultStatus.TooManyRequests => Results.StatusCode(429),
                _ => Results.Problem(result.Message ?? result.Errors?.FirstOrDefault(), statusCode: 500)
            };
        }
    }
}
