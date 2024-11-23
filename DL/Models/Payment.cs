namespace DL.Models;

/// <summary>
/// This table tracks rent and other payments made by tenants.
/// </summary>
public class Payment
{
    public int Id { get; set; }
    public int LeaseId { get; set; }
    public int TenantId { get; set; }

    public decimal PaymentAmount { get; set; } // The amount of the payment.
    public DateTime PaymentDate { get; set; } = DateTime.Now; // Date the payment was made.

    public string PaymentStatus { get; set; } // Status of the payment (e.g., paid, failed, pending).


    // Navigation properties
    public virtual Lease Lease { get; set; }    
    public virtual Tenant Tenant { get; set; }  

}
