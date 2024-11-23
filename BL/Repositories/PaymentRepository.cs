using BL.Interfaces;
using DL.Context;
using DL.Models;
namespace BL.Repositories;

public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
{
    public PaymentRepository(RealStateDbContext dbContext) : base(dbContext)
    {

    }
}