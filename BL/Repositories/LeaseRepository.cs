using BL.Interfaces;
using DL.Context;
using DL.Models;
namespace BL.Repositories;

public class LeaseRepository : GenericRepository <Lease> , ILeaseRepository
{
    public LeaseRepository(RealStateDbContext dbContext) : base (dbContext)
    {
        
    }
}
