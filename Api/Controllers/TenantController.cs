using BL.Interfaces;
using BL.Models;
using DL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenantController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        public TenantController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            this._unitOfWork = unitOfWork;
            this._userManager = userManager;
        }

        // GET: api/Tenant/GetAll
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllTenants()
        {
            var leases = await _unitOfWork.TenantRepository.GetAll();
            return Ok(leases);
        }



        // GET: api/Tenant/Get/{id}
        [HttpGet("Get/{id:int}")]
        public async Task<IActionResult> GetTenantById(int id)
        {
            var tenant = await _unitOfWork.TenantRepository.GetById(id);
            if (tenant == null)
            {
                return NotFound();
            }

            return Ok(tenant);
        }



        // PUT: api/Tenant/Edit/{id}
        [HttpPut("Edit/{id:int}")]
        public async Task<IActionResult> UpdateTenant(int id, [FromBody] TenantDTO model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingTenant = await _unitOfWork.TenantRepository.GetById(id);

            if (existingTenant == null)
            {
                return NotFound();
            }

            // Update the properties of the existing Tenant

            existingTenant.FullName = model.FullName;
            existingTenant.DateOfBirth = model.DateOfBirth;


            _unitOfWork.TenantRepository.Update(existingTenant);
            await _unitOfWork.Complete();

            return NoContent();



        }



        // DELETE: api/Tenant/Delete/{id}
        [HttpDelete("Delete/{id:int}")]
        public async Task<IActionResult> DeleteTenant(int id)
        {
            var tenant = await _unitOfWork.TenantRepository.GetById(id);
            if (tenant == null)
            {
                return NotFound();
            }

            var user=await _userManager.Users.FirstOrDefaultAsync(e=>e.Id==tenant.UserId);

            await _userManager.DeleteAsync(user);

            _unitOfWork.TenantRepository.Delete(tenant);
            await _unitOfWork.Complete();

            return NoContent();
        }


    }
}
