using AuthService.Domain.Interfaces;
using AuthService.Infrastructure.Persistence.DataContext;
using AuthService.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace AuthService.Infrastructure.Extensions
{
    public static class AuthInfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<AuthDbContext>(options =>
                    options.UseNpgsql(config.GetConnectionString("AuthDb")));

            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
