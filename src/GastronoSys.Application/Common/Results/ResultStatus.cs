namespace GastronoSys.Application.Common.Results
{
    public enum ResultStatus
    {
        Success = 200,
        Created = 201,
        Accepted = 202,
        NoContent = 204,
        BadRequest = 400,
        ValidationError = 422,
        Unauthorized = 401,
        Forbidden = 403,
        NotFound = 404,
        Conflict = 409,
        Gone = 410,
        TooManyRequests = 429,
        Error = 500,
        NotImplemented = 501,
        ServiceUnavailable = 503
    }
}
