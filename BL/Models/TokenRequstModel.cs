using System.ComponentModel.DataAnnotations;
namespace BL.Models;

public class TokenRequstModel
{
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    [DataType(DataType.Password)]
    public string Password { get; set; }
}
