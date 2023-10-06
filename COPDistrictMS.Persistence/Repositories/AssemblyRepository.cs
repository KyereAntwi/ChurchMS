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

    public async Task AddManagersToAssembly(Guid id, List<string> managersUsernames)
    {
        var assembly = await _dbContext.Assemblies.Include(a => a.Managers).FirstOrDefaultAsync(a => a.Id == id);
        foreach (var manager in managersUsernames)
        {
            assembly!.Managers.Add(manager);
        }
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Assembly?> GetAssemblyWithDistrict(Guid id) => await
        _dbContext
            .Assemblies
            .Include(a => a.District)
            .Include(a => a.Managers)
            .Include(a => a.PresidingOfficer)
            .FirstOrDefaultAsync(a => a.Id == id);

    public async Task<IReadOnlyList<Assembly>> GetManagedAssemblies(string username) =>
        await _dbContext.Assemblies
        .Include(a => a.Managers)
        .Where(a => a.Managers.Contains(username))
        .Select(a => new Assembly { Id = a.Id, Title = a.Title, Location = a.Location })
        .ToListAsync();
}