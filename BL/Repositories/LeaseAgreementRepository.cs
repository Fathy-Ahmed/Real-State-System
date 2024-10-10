using BL.Interfaces;
using DL.Context;
using DL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Repositories
{
    public class LeaseAgreementRepository : GenericRepository <LeaseAgreement> , ILeaseAgreementRepository
    {
        public LeaseAgreementRepository(RealStateDbContext dbContext) : base (dbContext)
        {
            
        }
    }
}
