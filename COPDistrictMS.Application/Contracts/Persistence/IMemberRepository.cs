using COPDistrictMS.Domain.Entities;

namespace COPDistrictMS.Application.Contracts.Persistence;

public interface IMemberRepository : IAsyncRepository<Member>
{
}
