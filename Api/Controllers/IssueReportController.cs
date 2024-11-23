using BL.DTO;
using BL.Interfaces;
using BL.Utilities;
using DL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles =SD.Tenant)]
public class IssueReportController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<ApplicationUser> _userManager;

    public IssueReportController
        (IUnitOfWork unitOfWork,UserManager<ApplicationUser> userManager)
    {
        _unitOfWork = unitOfWork;
        this._userManager = userManager;
    }


    // GET: api/IssueReport/GetAll
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAllIssueReports()
    {
        var issueReports = await _unitOfWork.IssueReportRepository.GetAll();
        return Ok(issueReports);
    }

    // GET: api/IssueReport/Get/{id}
    [HttpGet("Get/{id:int}")]
    public async Task<IActionResult> GetIssueReportById(int id)
    {
        var issueReport = await _unitOfWork.IssueReportRepository.GetById(id);
        if (issueReport == null)
        {
            return NotFound();
        }
        return Ok(issueReport);
    }


    // POST: api/IssueReport/Add
    [HttpPost("Add")]
    public async Task<IActionResult> CreateIssueReport([FromBody] IssueReportDTO IssuDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = await _userManager.Users.FirstOrDefaultAsync();
        var tenant = await _unitOfWork.TenantRepository.GetByUserId(user.Id);
        var issueReport = new IssueReport()
        {
            PropertyId=IssuDTO.PropertyId,
            TenantId= tenant.Id,
            IssueDescription= IssuDTO.IssueDescription,
            ReportedDate= IssuDTO.ReportedDate,
            ResolutionDate= IssuDTO.ResolutionDate,
            Status= IssuDTO.Status,
        };

        await _unitOfWork.IssueReportRepository.Add(issueReport);
        await _unitOfWork.Complete();

        return CreatedAtAction(nameof(GetIssueReportById), new { id = issueReport.Id }, issueReport);
    }


    // PUT: api/IssueReport/Edit/{id}
    [HttpPut("Edit/{id:int}")]
    public async Task<IActionResult> UpdateIssueReport(int id, [FromBody] IssueReportDTO IssuDTO)
    {

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var existingIssueReport = await _unitOfWork.IssueReportRepository.GetById(id);
        if (existingIssueReport == null)
        {
            return NotFound();
        }

        // Update the properties of the existing issue report
        existingIssueReport.PropertyId = IssuDTO.PropertyId;
        existingIssueReport.IssueDescription = IssuDTO.IssueDescription;
        existingIssueReport.ReportedDate = IssuDTO.ReportedDate;
        existingIssueReport.ResolutionDate = IssuDTO.ResolutionDate;
        existingIssueReport.Status = IssuDTO.Status;

        _unitOfWork.IssueReportRepository.Update(existingIssueReport);
        await _unitOfWork.Complete();

        return NoContent();
    }



    // DELETE: api/IssueReport/Delete/{id}
    [HttpDelete("Delete/{id:int}")]
    public async Task<IActionResult> DeleteIssueReport(int id)
    {
        var issueReport = await _unitOfWork.IssueReportRepository.GetById(id);
        if (issueReport == null)
        {
            return NotFound();
        }

        _unitOfWork.IssueReportRepository.Delete(issueReport);
        await _unitOfWork.Complete();

        return NoContent();
    }
}




