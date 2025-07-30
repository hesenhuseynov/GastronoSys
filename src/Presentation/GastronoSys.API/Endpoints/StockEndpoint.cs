using Asp.Versioning.Conventions;
using GastronoSys.API.Extensions;
using GastronoSys.Application.Features.StockItem.Commands;
using MediatR;

namespace GastronoSys.API.Endpoints
{
    public static class StockEndpoints
    {
        public static void MapStockEndpoints(this IEndpointRouteBuilder app)
        {
            var versionSet = app.NewApiVersionSet()
                .HasApiVersion(1.0)
                .ReportApiVersions()
                .Build();

            var group = app.MapGroup("/api/v{version:ApiVersion}/stocks")
                .WithApiVersionSet(versionSet);

            group.MapPost("/", async (CreateStockItemCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);

                return result.ToMinimalApiResult();

            }).WithName("CreateStockItemV1")
            .HasApiVersion(1.0)
            .Produces(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError);
        }
    }
}
