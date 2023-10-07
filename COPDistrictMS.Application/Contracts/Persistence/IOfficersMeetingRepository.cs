using COPDistrictMS.Domain.Entities;

namespace COPDistrictMS.Application.Contracts.Persistence;

public interface IOfficersMeetingRepository : IAsyncRepository<OfficersMeeting>
{
    Task<IReadOnlyList<OfficersMeeting>> GetMeetingsForDistrict(Guid districtId);
    Task<OfficersMeeting?> GetMeetingDetailsWithMembers(Guid meetingId);
}