using COPDistrictMS.Domain.Entities;

namespace COPDistrictMS.Application.Contracts.Persistence;

public interface IOfficersMeetingRepository : IAsyncRepository<OfficersMeeting>
{
    Task<(int, IReadOnlyList<OfficersMeeting>)> GetMeetingsForDistrict(Guid districtId, DateTime createdAt, string pastorInCharge, int page, int size);
    Task<OfficersMeeting?> GetMeetingDetailsWithMembers(Guid meetingId);
}