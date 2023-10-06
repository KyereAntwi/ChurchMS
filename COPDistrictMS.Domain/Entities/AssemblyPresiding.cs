namespace COPDistrictMS.Domain.Entities;

public class AssemblyPresiding
{
    public Guid Id { get; set; }

    public Guid AssemblyId { get; set; }
    public Assembly? Assembly { get; set; }

    public Guid MemberId { get; set; }
    public Member? Member { get; set; }
}
