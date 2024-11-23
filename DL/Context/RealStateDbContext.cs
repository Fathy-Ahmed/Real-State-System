using DL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace DL.Context;

public class RealStateDbContext : IdentityDbContext<ApplicationUser>
{

    public RealStateDbContext(DbContextOptions<RealStateDbContext> options) : base(options) { } 
    
    
    public DbSet<Property> Properties { get; set; }
    public DbSet<IssueReport> IssueReports { get; set; }
    public DbSet<Lease> Leases { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Tenant> Tenants { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Property-Lease relationship (One-to-Many)
        modelBuilder.Entity<Lease>()
           .HasOne(l => l.Property)
           .WithMany(p => p.Leases)
           .HasForeignKey(l => l.PropertyId);


        // Tenant-Lease relationship (One-to-One or One-to-Many depending on the design)
        modelBuilder.Entity<Lease>()
            .HasOne(l => l.Tenant)
            .WithMany(t => t.Leases)
            .HasForeignKey(l => l.TenantId);

        modelBuilder.Entity<Property>()
            .HasOne(p => p.Owner)
            .WithOne()
            .HasForeignKey<Property>(e => e.OwnerId);

        // Configure Tenant -> ApplicationUser relationship
            //modelBuilder.Entity<Tenant>()
            //    .HasOne(t => t.User)
            //    .WithMany() // Tenant is linked to ApplicationUser, not the other way around
            //    .HasForeignKey(t => t.UserId);

        

       


    }



}
