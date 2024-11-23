namespace BL.DTO;

public class PaymentDTO
{
    public int LeaseId { get; set; }
    public int TenantId { get; set; }

    public decimal PaymentAmount { get; set; } // The amount of the payment.
    public DateTime PaymentDate { get; set; } // Date the payment was made.

    public string PaymentStatus { get; set; } // Status of the payment (e.g., paid, failed, pending).

}
