namespace COPDistrictMS.Domain.Entities;

public class OfficerType : BaseEntity
{
    public string Office { get; set; } = string.Empty;
    public ICollection<Member> Members { get; set; } = default!;
}