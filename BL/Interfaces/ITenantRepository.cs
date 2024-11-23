using DL.Models;
namespace BL.Interfaces;

public interface ITenantRepository : IGenericRepository<Tenant>  
{
    Task<Tenant> GetByUserId (string userId);
}
