namespace DL.Models;

/// <summary>
/// This table tracks lease agreements for properties.
/// </summary>
public class Lease
{
    public int Id { get; set; }
    public int PropertyId { get; set; }   // Foreign Key to Property
    public int TenantId { get; set; }     // Foreign Key to Tenant

    public DateTime StartDate { get; set; } // 2024/11/01
    public DateTime EndDate { get; set; }   // 2025/10/30
    public DateTime PaymentDueDate { get; set; } // 2024/11/05 // The recurring date for rent payments.

    public decimal SecurityDeposit { get; set; } // Security deposit amount.
    public decimal RentAmount { get; set; }

    public string LeaseTerms { get; set; }



    public virtual Property Property { get; set; } // Navigation property

    public virtual Tenant Tenant { get; set; } // Navigation property

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

}
