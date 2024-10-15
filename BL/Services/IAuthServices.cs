using BL.Models;

namespace BL.Services;

public interface IAuthServices
{
    Task<AuthDTO> RegisterAsync(RegisterDTO model);
    Task<AuthDTO> GetTokenAsync(TokenRequstDTO model);
    Task<string> AddRoleAsync(AddRoleDTO model);
}
