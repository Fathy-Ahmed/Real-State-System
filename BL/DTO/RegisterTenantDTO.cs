using System.ComponentModel.DataAnnotations;
namespace BL.Models;

public class RegisterTenantDTO
{
    [StringLength(100)]
    public string UserName { get; set; }
    [StringLength(128)]
    public string Email { get; set; }
    [StringLength(256)]
    public string Password { get; set; }

    public string FullName { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime DateOfBirth { get; set; }
}