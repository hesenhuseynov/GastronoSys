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
