using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL.Models
{
    public class Property
    {
        public int PropertyId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Img { get; set; }
        public string Location { get; set; }
        public int Size { get; set; }  // Size in square meters
        public decimal Price { get; set; }


        // Navigation Properties
        public LeaseAgreement LeaseAgreement { get; set; }

    }
}
