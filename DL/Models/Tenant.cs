namespace DL.Models;

/// <summary>
/// This table stores information about the properties managed by the system.
/// </summary>
public class Tenant
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime DateOfBirth { get; set; }



    public virtual ApplicationUser User { get; set; }
    public virtual ICollection<Lease> Leases { get; set; } = new List<Lease>();
    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();


}
