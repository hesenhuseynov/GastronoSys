using GastronoSys.Domain.Entities;
using GastronoSys.Domain.Enums;
using Microsoft.Extensions.DependencyInjection;

namespace GastronoSys.Infrastructure.Persistence
{
    public static class DbSeeder
    {
        public static async Task SeedAdminandDemoAsync(IServiceProvider services)
        {
            using var scope = services.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<GastronoSysDbContext>();

            if (!context.Users.Any(u => u.UserName == "admin"))
            {
                var admin = new User
                {
                    UserName = "admin",
                    FullName = "System Admin ",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123"),
                    RoleId = 1,
                    Email = "admin@yourdomain.com"
                };
                context.Users.Add(admin);
            }

            if (!context.Tables.Any())
            {
                context.Tables.Add(new Table
                {
                    Name = "Masa 3 ",
                    StatusId = (int)TableStatusEnum.Available,
                    Capacity = 4
                });
            }


            if (!context.ProductCategories.Any())
            {
                var category = new ProductCategory
                {
                    Name = "Əsas yeməklər"
                };

                context.ProductCategories.Add(category);

                await context.SaveChangesAsync();

                context.Products.Add(new Product
                {
                    Name = "Toyuq qızartma",
                    Price = 8,
                    ProductCategoryId = category.Id,
                    IsActive = true,
                });
            }

            var statusList = Enum.GetValues(typeof(TableStatusEnum))
                .Cast<TableStatusEnum>()
                .Select(e => new TableStatus
                {
                    Id = (int)e,
                    Name = e.ToString()
                });

            foreach (var status in statusList)
            {
                if (!context.TableStatuses.Any(s => s.Id == status.Id))
                {
                    context.TableStatuses.Add(status);
                }

            }

            await context.SaveChangesAsync();
        }
    }
}
