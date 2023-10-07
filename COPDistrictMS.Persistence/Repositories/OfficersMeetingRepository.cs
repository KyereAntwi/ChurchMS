using COPDistrictMS.Application.Contracts.Persistence;
using COPDistrictMS.Domain.Entities;
using COPDistrictMS.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace COPDistrictMS.Persistence.Repositories;

public class OfficersMeetingRepository : BaseRepository<OfficersMeeting>, IOfficersMeetingRepository
{
    private readonly COPDistrictMSContext _dbContext;

    public OfficersMeetingRepository(COPDistrictMSContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<OfficersMeeting>> GetMeetingsForDistrict(Guid districtId) =>
        await _dbContext
            .OfficersMeetings
            .Where(m => m.DistrictId == districtId)
            .ToListAsync();

    public async Task<OfficersMeeting?> GetMeetingDetailsWithMembers(Guid meetingId) =>
        await _dbContext
            .OfficersMeetings
            .Include(m => m.Officers)
            .FirstOrDefaultAsync(m => m.Id == meetingId);
}