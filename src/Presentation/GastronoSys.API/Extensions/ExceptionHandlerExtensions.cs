using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace GastronoSys.API.Extensions
{
    public static class ExceptionHandlerExtensions
    {
        public static void UseCustomerExceptionHandler(this WebApplication app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

                    if (exception is BadHttpRequestException badReq)
                    {
                        var problem = new ProblemDetails
                        {
                            Status = 400,
                            Title = "Bad Request",
                            Detail = badReq.Message,
                            Instance = context.Request.Path
                        };
                        context.Response.StatusCode = 400;
                        context.Response.ContentType = "application/problem+json";
                        await context.Response.WriteAsJsonAsync(problem);
                        return;
                    }

                    if (exception is ValidationException validationException)
                    {
                        var errors = validationException.Errors
                        .GroupBy(e => e.PropertyName)
                        .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());


                        var validationProblemDetails = new ValidationProblemDetails(errors)
                        {
                            Status = StatusCodes.Status422UnprocessableEntity,
                            Title = "Valdiaton Error",
                            Detail = "On or more validation error occured",
                            Instance = context.Request.Path
                        };

                        context.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
                        context.Response.ContentType = "application/problem+json";
                        await context.Response.WriteAsJsonAsync(validationProblemDetails);

                        return;
                    }

                    var problemDetails = new ProblemDetails
                    {
                        Status = StatusCodes.Status500InternalServerError,
                        Title = "Server error occured",
                        Detail = exception?.Message,
                        Instance = context.Request.Path
                    };

                    context.Response.StatusCode = problemDetails.Status ?? 500;
                    context.Response.ContentType = "application/problem+json ";

                    await context.Response.WriteAsJsonAsync(problemDetails);
                });
            });
        }

    }
}
