using COPDistrictMS.Application.Contracts.Persistence;
using COPDistrictMS.Domain.Entities;
using COPDistrictMS.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace COPDistrictMS.Persistence.Repositories;

public class OfficerRepository : BaseRepository<OfficerType>, IOfficerRepository
{
    private readonly COPDistrictMSContext _dbContext;

    public OfficerRepository(COPDistrictMSContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<OfficerType?> GetByType(string type) =>
        await _dbContext.OfficerTypes.FirstOrDefaultAsync(o => o.Office == type);

    public async Task<OfficerType?> GetOfficeWithMembersAsync(Guid officeTypeId) =>
        await _dbContext
            .OfficerTypes
            .Include(o => o.Members)
            .FirstOrDefaultAsync(o => o.Id == officeTypeId);
}