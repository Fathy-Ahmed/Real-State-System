using DL.Models;

namespace BL.DTO;

public class IssueReportDTO
{
    public int PropertyId { get; set; }

    public string IssueDescription { get; set; }
    public DateTime ReportedDate { get; set; }
    public DateTime? ResolutionDate { get; set; }

    public string Status { get; set; } // e.g., Pending, Resolved

}
