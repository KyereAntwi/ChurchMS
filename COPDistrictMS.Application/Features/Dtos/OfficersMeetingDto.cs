namespace COPDistrictMS.Application.Features.Dtos;

public class OfficersMeetingDto
{
    public Guid Id { get; set; }
    public string PastorInCharge { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; } = String.Empty;

    public IEnumerator<Guid> MembersId { get; set; } = default!;
}