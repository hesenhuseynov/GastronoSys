using FluentValidation;
using GastronoSys.Application.Features.Orders.BusinessRules;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;

namespace GastronoSys.Application
{
    public static class ApplicationServiceRegistration
    {

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AssemblyMarker).Assembly));

            services.AddValidatorsFromAssembly(typeof(AssemblyMarker).Assembly);

            var config = TypeAdapterConfig.GlobalSettings;

            config.Scan(typeof(AssemblyMarker).Assembly);

            services.AddSingleton(config);
            services.AddScoped<IMapper, ServiceMapper>();

            services.AddScoped<OrderBusinessRules>();

            return services;

        }


    }
}
