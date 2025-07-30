using Asp.Versioning.Conventions;
using GastronoSys.API.Extensions;
using GastronoSys.Application.Features.Products.Commands;
using GastronoSys.Application.Features.Products.Dtos;
using MediatR;

namespace GastronoSys.API.Endpoints
{
    public static class ProductEndpoints
    {
        public static void MapProductEndpoints(this IEndpointRouteBuilder app)
        {
            var versionSet = app.NewApiVersionSet()
                .HasApiVersion(1.0)
                .ReportApiVersions()
                .Build();

            var group = app.MapGroup("/api/v{version:ApiVersion}/products")
                .WithApiVersionSet(versionSet);

            group.MapPost("/", async (CreateProductCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);
                return result.ToMinimalApiResult();

            }).WithName("CreateProductV1")
            .HasApiVersion(1.0)
            .Produces<ProductDto>(StatusCodes.Status201Created)
            .Produces<HttpValidationProblemDetails>(StatusCodes.Status422UnprocessableEntity)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError);
        }
    }
}
