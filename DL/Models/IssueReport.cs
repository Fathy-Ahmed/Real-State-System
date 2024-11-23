namespace DL.Models;

/// <summary>
/// This table records issues reported by tenants related to their properties.
/// </summary>
public class IssueReport
{
    public int Id { get; set; }
    public int TenantId { get; set; }
    public int PropertyId { get; set; }

    public string IssueDescription { get; set; }
    public DateTime ReportedDate { get; set; }
    public DateTime? ResolutionDate { get; set; }

    public string Status { get; set; } // e.g., Pending, Resolved

    public virtual Tenant Tenant { get; set; } // Navigation property
    public virtual Property Property { get; set; } // Navigation property

}