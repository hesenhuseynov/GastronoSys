using Asp.Versioning.Conventions;
using GastronoSys.API.Extensions;
using GastronoSys.Application.Features.ProductIngredients.Commands;
using MediatR;

namespace GastronoSys.API.Endpoints
{
    public static class ProductIngredientEndpoint
    {
        public static void MapProductIngredientEndpoints(this IEndpointRouteBuilder app)
        {
            var versionSet = app.NewApiVersionSet()
                .HasApiVersion(1.0)
                .ReportApiVersions()
                .Build();

            var group = app.MapGroup("/api/v{version:ApiVersion}/product-ingredients")
                .WithApiVersionSet(versionSet);

            group.MapPost("/", async (CreateProductIngredientCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);

                return result.ToMinimalApiResult();

            }).WithName("CreateProductIngredientV1")
                .HasApiVersion(1.0)
                .Produces(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status422UnprocessableEntity)
                .ProducesProblem(StatusCodes.Status500InternalServerError);
        }
    }
}
