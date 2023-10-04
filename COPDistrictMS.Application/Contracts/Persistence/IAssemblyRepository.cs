using COPDistrictMS.Domain.Entities;

namespace COPDistrictMS.Application.Contracts.Persistence;

public interface IAssemblyRepository : IAsyncRepository<Assembly>
{
    Task<Assembly?> GetAssemblyWithDistrict(Guid id);
}
