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
public class ManagerController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<ApplicationUser> _userManager;
    public ManagerController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
    {
        this._unitOfWork = unitOfWork;
        _userManager = userManager;
    }


    // GET: api/Manager/FinancialReporting
    [HttpGet("FinancialReporting")]
    public async Task<IActionResult> FinancialReporting()
    {
        var user = await _userManager.Users.FirstOrDefaultAsync();


        return Ok(user);
    }

    // GET: api/Manager/RentPayments
    [HttpGet("RentPayments")]
    [Authorize(Roles =SD.Manager)]
    public async Task<IActionResult> RentPayments()
    {
        var user = await _userManager.Users.FirstOrDefaultAsync();

        if (user == null) {
            return NoContent();
        }

        var tenants=(await _unitOfWork.TenantRepository.GetAll()).Where(e=>e.UserId==user.Id).ToList();
        List<Payment> payments = new List<Payment>();

        foreach (var tenant in tenants)
        {
            payments.AddRange((await _unitOfWork.PaymentRepository.GetAll()).
                Where(e => (e.TenantId == tenant.Id  && e.PaymentStatus==SD.PaymentPaid )));
        }

        return Ok(payments);
    }

    // GET: api/Manager/OutstandingPayments
    [HttpGet("OutstandingPayments")]
    [Authorize(Roles = SD.Manager)]
    public async Task<IActionResult> OutstandingPayments()
    {
        var user = await _userManager.Users.FirstOrDefaultAsync();

        if (user == null)
        {
            return NoContent();
        }

        var tenants = (await _unitOfWork.TenantRepository.GetAll()).Where(e => e.UserId == user.Id).ToList();
        List<Payment> payments = new List<Payment>();

        foreach (var tenant in tenants)
        {
            var leases= (await _unitOfWork.LeaseRepository.GetAll()).Where(e=>e.TenantId==tenant.Id);
            foreach (var lease in leases)
            {
                var paymentsNumber =(await _unitOfWork.PaymentRepository.GetAll()).Where(e=>e.TenantId==tenant.Id && e.LeaseId==lease.Id).Count();
                var monthsNumber = ((lease.EndDate.Year - lease.StartDate.Year) * 12) + (lease.EndDate.Month - lease.StartDate.Month);
                var currentMonth =DateTime.Now.Month;
                var currentDay =DateTime.Now.Day;
                var housingMonth = ((DateTime.Now.Year - lease.StartDate.Year) * 12) + (currentMonth - lease.StartDate.Month);
            }
            payments.AddRange((await _unitOfWork.PaymentRepository.GetAll()).Where(e => e.TenantId == tenant.Id));
        }

        return Ok(payments);
    }

}
