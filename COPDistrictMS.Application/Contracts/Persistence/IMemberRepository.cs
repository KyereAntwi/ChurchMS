using COPDistrictMS.Domain.Entities;

namespace COPDistrictMS.Application.Contracts.Persistence;

public interface IMemberRepository : IAsyncRepository<Member>
{
    Task<Member> GetFullDetailsAsync(Guid memberId);
    Task<(IReadOnlyList<Member>, int)> GetMembersByAssembly(Guid assemblyId, int page, int size);
    Task<(IReadOnlyList<Member>, int)> FilterMembersByDistrict(
        Guid assemblyId, 
        Guid districtId, 
        string nameString, 
        Guid officeTypeId, 
        DateOnly dateOfBirth,
        int year,
        int month,
        int page,
        int size);
}
