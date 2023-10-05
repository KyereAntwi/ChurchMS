using COPDistrictMS.Application.Commons;
using Microsoft.Extensions.DependencyInjection;

namespace COPDistrictMS.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddMediatR(req =>
        {
            req.RegisterServicesFromAssemblyContaining<MappingProfiles>();
        });
        
        return services;
    }
}