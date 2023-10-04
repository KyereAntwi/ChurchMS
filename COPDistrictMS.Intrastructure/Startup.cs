using COPDistrictMS.Application.Contracts.Infrastructure;
using COPDistrictMS.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace COPDistrictMS.Infrastructure;

public static class Startup
{
    public static IServiceCollection RegisterPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        // add services
        services.AddSingleton<IImageService, ImageService>();

        return services;
    }
}
