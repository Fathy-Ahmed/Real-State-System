using BL.Interfaces;
using BL.Models;
using BL.Utilities;
using DL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaseController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public LeaseController(IUnitOfWork unitOfWork,UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            this._userManager = userManager;
        }

        // GET: api/Lease/GetAll
        [HttpGet("GetAll")]
        [Authorize(Roles =SD.Manager)]
        public async Task<IActionResult> GetAllLeases()
        {
            var leases = await _unitOfWork.LeaseRepository.GetAll();
            return Ok(leases);
        }

        // GET: api/Lease/GetAllForTenant
        [HttpGet("GetAllForTenant")]
        [Authorize(Roles = SD.Tenant)]
        public async Task<IActionResult> GetAllLeasesForTenant()
        {
            var user = await _userManager.Users.FirstOrDefaultAsync();
            var tenant = (await _unitOfWork.TenantRepository.GetByUserId(user.Id));
            var leases = (await _unitOfWork.LeaseRepository.GetAll()).Where(e=>e.TenantId==tenant.Id);
            return Ok(leases);
        }


        // GET: api/Lease/Get/{id}
        [HttpGet("Get/{id:int}")]
        [Authorize(Roles = SD.Manager)]
        public async Task<IActionResult> GetLeaseById(int id)
        {
            var lease = await _unitOfWork.LeaseRepository.GetById(id);
            if (lease == null)
            {
                return NotFound();
            }

            return Ok(lease);
        }


        // POST: api/Lease/Add
        [HttpPost("Add")]
        [Authorize(Roles = SD.Manager)]
        public async Task<IActionResult> CreateLease([FromBody] LeaseDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var property = (await _unitOfWork.PropertyRepository.GetById(model.PropertyId));
            if (property == null)
            {
                ModelState.AddModelError("PropertyId", "Property Not Found");
                return BadRequest(ModelState);
            }
            else if (property.AvailabilityStatus == SD.AvailabilityRented)
            {
                ModelState.AddModelError("PropertyId", "Property Is Already Rented");
                return BadRequest(ModelState);
            }
            var tenant = (await _unitOfWork.TenantRepository.GetById(model.TenantId));
            if (tenant == null)
            {
                ModelState.AddModelError("TenantId", "Tenant Not Found");
                return BadRequest(ModelState);
            }

            Lease lease = new()
            {
                PropertyId = model.PropertyId,
                TenantId = model.TenantId,
                StartDate= model.StartDate,
                EndDate= model.EndDate,
                LeaseTerms=model.LeaseTerms,
                PaymentDueDate=model.PaymentDueDate,
                RentAmount=model.RentAmount,
                SecurityDeposit=model.SecurityDeposit,
            };

            property.AvailabilityStatus=SD.AvailabilityRented;
            //HttpStatusCode.Ba
            await _unitOfWork.LeaseRepository.Add(lease);
            await _unitOfWork.Complete();

            return CreatedAtAction(nameof(GetLeaseById), new { id = lease.Id }, model);
        }

        // PUT: api/Lease/Edit/{id}
        [HttpPut("Edit/{id:int}")]

        [Authorize(Roles = SD.Manager)]
        public async Task<IActionResult> UpdateLease(int id, [FromBody] LeaseDTO lease)
        {
            //if (id != lease.Id)
            //{
            //    return BadRequest("Lease ID mismatch");
            //}

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingLease = await _unitOfWork.LeaseRepository.GetById(id);

            if (existingLease == null)
            {
                return NotFound();
            }

            // Update the properties of the existing lease
            existingLease.PropertyId = lease.PropertyId;
            existingLease.TenantId = lease.TenantId;
            existingLease.StartDate = lease.StartDate;
            existingLease.EndDate = lease.EndDate;
            existingLease.PaymentDueDate = lease.PaymentDueDate;
            existingLease.SecurityDeposit = lease.SecurityDeposit;
            existingLease.RentAmount = lease.RentAmount;
            existingLease.LeaseTerms = lease.LeaseTerms;

            _unitOfWork.LeaseRepository.Update(existingLease);
            await _unitOfWork.Complete();

            return NoContent();



        }



        // DELETE: api/Lease/Delete/{id}
        [HttpDelete("Delete/{id:int}")]
        [Authorize(Roles = SD.Manager)]
        public async Task<IActionResult> DeleteLease(int id)
        {
            var lease = await _unitOfWork.LeaseRepository.GetById(id);
            if (lease == null)
            {
                return NotFound();
            }

            _unitOfWork.LeaseRepository.Delete(lease);
            await _unitOfWork.Complete();

            return NoContent();
        }




    }
}
