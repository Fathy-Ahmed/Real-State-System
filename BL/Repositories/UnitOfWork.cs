using BL.Interfaces;
using DL.Context;
namespace BL.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly RealStateDbContext _dbContext;

    public IPropertyRepository PropertyRepository { get; set; }
    public ITenantRepository TenantRepository { get; set; }
    public ILeaseRepository LeaseRepository { get; set; }
    public IIssueReportRepository IssueReportRepository { get; set; }
    public IPaymentRepository PaymentRepository { get; set; }
    public UnitOfWork(RealStateDbContext dbContext)
    {
        PropertyRepository = new PropertyRepository(dbContext);
        TenantRepository = new TenantRepository(dbContext);
        LeaseRepository = new LeaseRepository(dbContext);
        IssueReportRepository = new IssueReportRepository(dbContext);
        PaymentRepository = new PaymentRepository(dbContext);

        _dbContext = dbContext;
    }
    public async Task<int> Complete()
    {
        return await _dbContext.SaveChangesAsync();
    }

}
