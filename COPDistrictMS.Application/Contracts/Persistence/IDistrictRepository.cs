using COPDistrictMS.Domain.Entities;

namespace COPDistrictMS.Application;

public interface IDistrictRepository : IAsyncRepository<District>
{
    Task<IReadOnlyList<District>> GetAllOwned(string userId);
    Task<IReadOnlyList<Guid>> GetDistrictAssemblyIds(Guid districtId);
    Task<District?> GetDistrictWithAssembliesFull(Guid districtId);
}
