using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL.Models
{
    public class LeaseAgreement
    {
        public int LeaseAgreementId { get; set; }
        public int PropertyId { get; set; }   // Foreign Key to Property
        public int TenantId { get; set; }     // Foreign Key to Tenant
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal RentAmount { get; set; }

        // Navigation Properties
        public Property Property { get; set; }
        public Tenant Tenant { get; set; }
    }
}
