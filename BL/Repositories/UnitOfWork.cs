using BL.Interfaces;
using DL.Context;

namespace BL.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly RealStateDbContext _dbContext;

    public IPropertyRepository PropertyRepository { get; set; }
    public ITenantRepository TenantRepository { get; set; }
    public ILeaseAgreementRepository LeaseAgreementRepository { get; set; }
    public UnitOfWork(RealStateDbContext dbContext)
    {
        PropertyRepository = new PropertyRepository(dbContext);
        TenantRepository = new TenantRepository(dbContext);
        LeaseAgreementRepository = new LeaseAgreementRepository(dbContext);

        _dbContext = dbContext;
    }
    public async Task<int> Complete()
    {
        return await _dbContext.SaveChangesAsync();
    }

}
