using COPDistrictMS.Application;
using COPDistrictMS.Application.Contracts.Persistence;
using COPDistrictMS.Persistence.Data;
using COPDistrictMS.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace COPDistrictMS.Persistence;

public static class Startup
{
    public static IServiceCollection RegisterPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<COPDistrictMSContext>(options => options.UseSqlite("Data Source=COPDistrictMS.db", 
            b => b.MigrationsAssembly("COPDistrictMS.Presentation.WebApi")));

        // add services
        services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));
        services.AddScoped<IDistrictRepository, DistrictRepository>();
        services.AddScoped<IAssemblyRepository, AssemblyRepository>();
        services.AddScoped<IMemberRepository, MemberRepository>();
        services.AddScoped<IOfficerRepository, OfficerRepository>();

        return services;
    }
}
