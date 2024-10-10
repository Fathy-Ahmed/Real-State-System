using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface IUnitOfWork
    {
        public IPropertyRepository  PropertyRepository { get; set; }
        public ITenantRepository TenantRepository { get; set; }
        public ILeaseAgreementRepository LeaseAgreementRepository { get; set; }
    }
}
