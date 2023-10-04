using COPDistrictMS.Domain.Entities;

namespace COPDistrictMS.Application.Contracts.Persistence;

public interface IOfficerRepository : IAsyncRepository<OfficerType>
{
    Task<OfficerType> GetByType(String type);
    Task<OfficerType> GetOfficeWithMembersAsync(Guid officeTypeId);
}
