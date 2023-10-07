namespace COPDistrictMS.Domain.Entities;

public class OfficersMeeting : BaseEntity
{
    public Guid DistrictId { get; set; }
    public District? District { get; set; }

    public string PastorInCharge { get; set; } = String.Empty;

    public ICollection<Member> Officers { get; set; } = default!;
}