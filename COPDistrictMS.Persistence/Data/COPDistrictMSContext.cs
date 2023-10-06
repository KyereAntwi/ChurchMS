using COPDistrictMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace COPDistrictMS.Persistence.Data;

public class COPDistrictMSContext : DbContext
{
    public COPDistrictMSContext(DbContextOptions<COPDistrictMSContext> options) : base(options)
    {
    }

    public DbSet<District> Districts { get; set; } = default!;
    public DbSet<Assembly> Assemblies { get; set; } = default!;
    public DbSet<OfficerType> OfficerTypes { get; set; } = default!;
    public DbSet<Member> Members { get; set; } = default!;
    public DbSet<AssemblyOffering> AssemblyOfferings { get; set; } = default!;
    public DbSet<MinistryOffering> MinistryOfferings { get; set; } = default!;
}
