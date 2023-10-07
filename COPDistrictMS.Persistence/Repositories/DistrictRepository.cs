using COPDistrictMS.Application;
using COPDistrictMS.Domain.Entities;
using COPDistrictMS.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace COPDistrictMS.Persistence.Repositories;

public class DistrictRepository : BaseRepository<District>, IDistrictRepository
{
    private readonly COPDistrictMSContext _dbContext;

    public DistrictRepository(COPDistrictMSContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<District>> GetAllOwned(string userId) => 
        await _dbContext.Districts.Where(d => d.CreatedBy == userId).ToListAsync();

    public async Task<IReadOnlyList<Guid>> GetDistrictAssemblyIds(Guid districtId)
    {
        var list = await GetDistrictWithAssembliesFull(districtId);
        return list!.Assemblies.Select(a => a.Id).ToList();
    }

    public async Task<District?> GetDistrictWithAssembliesFull(Guid districtId) => await
        _dbContext
            .Districts
            .Include(d => d.Assemblies)
            .FirstOrDefaultAsync(d => d.Id == districtId);

    public async Task<(int, IReadOnlyList<MinistryOffering>)> GetDistrictOfferings(Guid districtId, string ministry, int page, int size)
    {
        var list = _dbContext.MinistryOfferings
            .Where(o => o.DistrictId == districtId)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(ministry))
        {
            list = list.Where(l => l.Ministry.ToLower().Contains(ministry.ToLower()));
        }

        var listToSend = list.Skip((page - 1) * size).Take(size)
            .AsNoTracking();

        return (list.Count(), await listToSend.ToListAsync());
    }
}