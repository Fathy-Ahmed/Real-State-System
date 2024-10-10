using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL.Models
{
    public class Tenant
    {
        public int TenantId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        // Navigation Properties
        public ICollection<LeaseAgreement> LeaseAgreements { get; set; }
        //public ICollection<Payment> Payments { get; set; }
    }
}
