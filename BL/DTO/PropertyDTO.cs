using Microsoft.AspNetCore.Http;

namespace BL.DTO;

public class PropertyDTO
{
    public string Name { get; set; }
    public string PropertyType { get; set; } //Type of property (e.g., residential, commercial).
    public string? Description { get; set; }
    public string Address { get; set; }
    public double SquareFootage { get; set; }  // Size of the property.
    public decimal RentPrice { get; set; } // Monthly rent.
    public string AvailabilityStatus { get; set; }
    public string? Img { get; set; } // ** in future add list of photos **


    public IFormFile? file { get; set; }
}
