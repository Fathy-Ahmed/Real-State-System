using BL.Models;
namespace BL.Services;

public interface IAuthServices
{
    Task<AuthDTO> RegisterTenantAsync(RegisterTenantDTO model);
    Task<AuthDTO> RegisterManagerAsync(RegisterManagerDTO model);
    Task<AuthDTO> GetTokenAsync(TokenRequstDTO model);
    Task<string> AddRoleAsync(AddRoleDTO model);
}
