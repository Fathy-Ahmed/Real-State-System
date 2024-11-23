using BL.Interfaces;
using DL.Context;
using DL.Models;
namespace BL.Repositories;

public class PropertyRepository : GenericRepository<Property> , IPropertyRepository
{
    public PropertyRepository(RealStateDbContext dbContext) : base (dbContext)
    {
        
    }
}
