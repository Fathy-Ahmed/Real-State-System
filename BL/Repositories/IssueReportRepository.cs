using BL.Interfaces;
using DL.Context;
using DL.Models;
namespace BL.Repositories;

public class IssueReportRepository : GenericRepository<IssueReport>, IIssueReportRepository
{
    public IssueReportRepository(RealStateDbContext dbContext) : base(dbContext)
    {

    }
}
