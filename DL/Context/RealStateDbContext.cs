using DL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DL.Context;

public class RealStateDbContext : IdentityDbContext<ApplicationUser>
{

    public RealStateDbContext(DbContextOptions<RealStateDbContext> options) : base(options) { } 
    
    
    public DbSet<Property> Properties { get; set; }
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<LeaseAgreement> LeaseAgreements { get; set; }

    //public DbSet<Payment> Payments { get; set; }
    


    

}
