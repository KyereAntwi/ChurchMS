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


    public async Task<(int, IReadOnlyList<OfficersMeeting>)> GetMeetingsForDistrict(
        Guid districtId, 
        DateTime createdAt, 
        string pastorInCharge, 
        int page, 
        int size)
    {
         var list = _dbContext
            .OfficersMeetings
            .Where(m => m.DistrictId == districtId)
            .AsQueryable();

         if (createdAt != new DateTime())
         {
             list = list.Where(m => m.CreatedAt.Date == createdAt.Date);
         }

         if (!string.IsNullOrWhiteSpace(pastorInCharge))
         {
             list = list.Where(m => m.PastorInCharge.ToLower().Contains(pastorInCharge.ToLower()));
         }

         var listToSend = list.Skip((page - 1) * size).Take(size)
             .AsNoTracking()
             .OrderBy(m => m.CreatedAt);

         return (list.Count(), await listToSend.ToListAsync());
    }

    public async Task<OfficersMeeting?> GetMeetingDetailsWithMembers(Guid meetingId) =>
        await _dbContext
            .OfficersMeetings
            .Include(m => m.Officers)
            .FirstOrDefaultAsync(m => m.Id == meetingId);
}