using Microsoft.Extensions.DependencyInjection;

namespace COPDistrictMS.Application;

public static class Start
{
    public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddMediatR(req =>
        {
            
        });
        
        return services;
    }
}