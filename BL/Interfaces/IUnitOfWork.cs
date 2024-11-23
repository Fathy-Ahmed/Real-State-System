namespace BL.Interfaces;

public interface IUnitOfWork
{
    public IPropertyRepository  PropertyRepository { get; set; }
    public ITenantRepository TenantRepository { get; set; }
    public ILeaseRepository LeaseRepository { get; set; }
    public IIssueReportRepository IssueReportRepository { get; set; }
    public IPaymentRepository PaymentRepository { get; set; }
    public Task<int> Complete();
}


