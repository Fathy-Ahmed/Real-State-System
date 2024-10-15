namespace BL.Interfaces;

public interface IUnitOfWork
{
    public IPropertyRepository  PropertyRepository { get; set; }
    public ITenantRepository TenantRepository { get; set; }
    public ILeaseAgreementRepository LeaseAgreementRepository { get; set; }
    public Task<int> Complete();
}


