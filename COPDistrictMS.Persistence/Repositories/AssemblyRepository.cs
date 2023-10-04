using COPDistrictMS.Application.Contracts.Persistence;
using COPDistrictMS.Domain.Entities;
using COPDistrictMS.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace COPDistrictMS.Persistence.Repositories;

public class AssemblyRepository : BaseRepository<Assembly>, IAssemblyRepository
{
    private readonly COPDistrictMSContext _dbContext;

    public AssemblyRepository(COPDistrictMSContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Assembly?> GetAssemblyWithDistrict(Guid id) => await
        _dbContext
            .Assemblies
            .Include(a => a.District)
            .FirstOrDefaultAsync(a => a.Id == id);
}