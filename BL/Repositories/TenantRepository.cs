using BL.Interfaces;
using DL.Context;
using DL.Models;
using Microsoft.EntityFrameworkCore;

namespace BL.Repositories;

public class TenantRepository : GenericRepository<Tenant> , ITenantRepository 
{
    private readonly RealStateDbContext _dbContext;

    public TenantRepository(RealStateDbContext dbContext) : base(dbContext) 
    {
        this._dbContext = dbContext;
    }

    public async Task<Tenant> GetByUserId(string userId)
    {
        return (await _dbContext.Tenants.Where(e => e.UserId == userId).FirstOrDefaultAsync());
    }
}
