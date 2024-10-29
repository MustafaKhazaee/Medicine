
using Medicine.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Medicine.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace Medicine.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure (this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContextPool<IApplicationDbContext, ApplicationDbContext>(
            options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("Medicine")!);
                options.EnableDetailedErrors();
            }
        );
    }
}
