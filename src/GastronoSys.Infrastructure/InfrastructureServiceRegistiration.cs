using GastronoSys.Domain.Repositories;
using GastronoSys.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace GastronoSys.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<ITableRepository, TableRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();

            //services.AddScoped<ICustomerRepository, CustomerRep>();

            return services;
        }

    }
}
