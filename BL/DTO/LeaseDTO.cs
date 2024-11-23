namespace BL.Models;

public class LeaseDTO
{
    public int PropertyId { get; set; }  
    public int TenantId { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime PaymentDueDate { get; set; } 

    public decimal SecurityDeposit { get; set; } 
    public decimal RentAmount { get; set; }

    public string LeaseTerms { get; set; }
}


public class TenantDTO
{
    public string FullName { get; set; }
    public DateTime DateOfBirth { get; set; }
}