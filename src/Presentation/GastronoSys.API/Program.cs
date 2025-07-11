using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using GastronoSys.API.Endpoints;
using GastronoSys.API.Extensions;
using GastronoSys.API.Helpers;
using GastronoSys.Application;
using GastronoSys.Infrastructure;
using GastronoSys.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace GastronoSys.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Host.UseSerilog((context, services, configuration) =>
            {
                configuration.ReadFrom.Configuration(context.Configuration)
                 .Enrich.FromLogContext();
            });



            builder.Services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            })
               .AddApiExplorer(options =>
               {
                   options.GroupNameFormat = "'v'VVV";

                   options.SubstituteApiVersionInUrl = true;
               });

            builder.Services.AddAuthorization();
            builder.Services.AddInfrastructureServices();
            builder.Services.AddApplicationServices();


            builder.Services.AddDbContext<GastronoSysDbContext>(options =>
           options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
          );

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen();
            builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();

            var app = builder.Build();
            Log.Information("API started");


            var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

            await DbSeeder.SeedAdminandDemoAsync(app.Services);

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();

                app.UseSwagger();

                app.UseSwaggerUI(options =>
                {
                    foreach (var desc in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint($"/swagger/{desc.GroupName}/swagger.json", desc.GroupName.ToUpperInvariant());
                    }
                });

            }

            app.UseCustomerExceptionHandler();

            app.UseHttpsRedirection();
            app.UseAuthorization();

            OrderEndpoints.MapOrderEndpoints(app);

            app.Run();
        }
    }
}
