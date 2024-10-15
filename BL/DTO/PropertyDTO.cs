using Microsoft.AspNetCore.Http;

namespace BL.DTO;

public class PropertyDTO
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? Img { get; set; }
    public string Location { get; set; }
    public int Size { get; set; }  // Size in square meters
    public decimal Price { get; set; }




    public IFormFile? file { get; set; }
}
