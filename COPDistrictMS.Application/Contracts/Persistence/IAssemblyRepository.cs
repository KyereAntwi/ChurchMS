using COPDistrictMS.Domain.Entities;

namespace COPDistrictMS.Application.Contracts.Persistence;

public interface IAssemblyRepository : IAsyncRepository<Assembly>
{
    Task<Assembly?> GetAssemblyWithDistrict(Guid id);
    Task AddManagersToAssembly(Guid id, List<string> managersUsernames);
    Task<IReadOnlyList<Assembly>> GetManagedAssemblies(string username);
    Task<(int, IReadOnlyList<AssemblyOffering>)> GetAssemblyOfferings(Guid assemblyId, string offeringType, int page, int size);
}
