using System.ComponentModel.DataAnnotations;

namespace COPDistrictMS.Domain.Entities;

public class Manager
{
    [Key]
    public string Username { get; set; } = string.Empty;
}
