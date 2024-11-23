namespace DL.Models;

/// <summary>
/// This table stores information about the properties managed by the system.
/// </summary>
public class Property
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string PropertyType { get; set; } //Type of property (e.g., residential, commercial).
    public string Description { get; set; }
    public string Address { get; set; }
    public double SquareFootage { get; set; }  // Size of the property.
    public decimal RentPrice { get; set; } // Monthly rent.
    public string AvailabilityStatus { get; set; } // Status (e.g., available, rented, under maintenance).
    
    public string? Img { get; set; } // ** in future add list of photos **

    
    public string? OwnerId { get; set; } // Foreign key to the AspNetUsers table (the owner of the property, could be a property manager).
    public virtual ApplicationUser Owner { get; set; } // Navigation Properties


    public virtual ICollection<Lease> Leases { get; set; } = new List<Lease>();

}
