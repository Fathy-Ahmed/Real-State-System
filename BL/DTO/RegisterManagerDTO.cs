using System.ComponentModel.DataAnnotations;
namespace BL.Models;

public class RegisterManagerDTO
{
    [StringLength(100)]
    public string UserName { get; set; }
    [StringLength(128)]
    public string Email { get; set; }
    [StringLength(256)]
    public string Password { get; set; }

}


