namespace COPDistrictMS.Domain.Entities;

public class Offering : BaseEntity
{
    public decimal Amount { get; set; }
    
    public ICollection<OfferingAttachment> Attachments { get; set; } = default!;
}
