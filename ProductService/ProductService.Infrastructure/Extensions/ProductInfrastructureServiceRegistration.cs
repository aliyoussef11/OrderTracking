using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductService.Domain.Interfaces;
using ProductService.Infrastructure.Persistence.DataContext;
using ProductService.Infrastructure.Persistence.Repositories;

namespace ProductService.Infrastructure.Extensions
{
    public static class ProductInfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<ProductDbContext>(options =>
                    options.UseNpgsql(config.GetConnectionString("ProductDb")));

            services.AddScoped<IProductRepository, ProductRepository>();

            return services;
        }
    }
}
