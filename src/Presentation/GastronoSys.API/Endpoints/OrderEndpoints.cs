using Asp.Versioning.Conventions;
using GastronoSys.API.Extensions;
using GastronoSys.Application.Common.Results;
using GastronoSys.Application.Features.Orders.Commands;
using MediatR;

namespace GastronoSys.API.Endpoints
{
    public static class OrderEndpoints
    {
        public static void MapOrderEndpoints(this IEndpointRouteBuilder app)
        {
            var versionSet = app.NewApiVersionSet()
                .HasApiVersion(1.0)
                .ReportApiVersions()
                .Build();

            var group = app.MapGroup("/api/v{version:Apiversion}/orders")
                .WithApiVersionSet(versionSet);

            group.MapPost("/", async (CreateOrderCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);

                //location header for  
                if (result.Status == ResultStatus.Created && result.Value is not null)
                    return Results.Created($"/api/v1/orders/{result.Value.Id}", result.Value);


                return result.ToMinimalApiResult();
            })
                .WithName("CreateOrderV1")
                .MapToApiVersion(1.0)
                .Produces(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status422UnprocessableEntity)
                .ProducesProblem(StatusCodes.Status500InternalServerError);

        }
    }
}
