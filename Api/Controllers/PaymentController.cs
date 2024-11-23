using BL.DTO;
using BL.Interfaces;
using DL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public PaymentController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }


        // GET: api/payments/GetAll
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Payment>>> GetPayments()
        {
            var payments = await _unitOfWork.PaymentRepository.GetAll();
            return Ok(payments);
        }


        // GET: api/payments/Get/5
        [HttpGet("Get/{id:int}")]
        public async Task<ActionResult<Payment>> GetPayment(int id)
        {
            var payment = await _unitOfWork.PaymentRepository.GetById(id);

            if (payment == null)
            {
                return NotFound();
            }

            return Ok(payment);
        }


        // POST: api/payments/Add
        [HttpPost("Add")]
        public async Task<ActionResult<Payment>> PostPayment(PaymentDTO paymentDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var payment = new Payment()
            {
                TenantId = paymentDTO.TenantId,
                LeaseId = paymentDTO.LeaseId,
                PaymentAmount= paymentDTO.PaymentAmount,
                PaymentDate = paymentDTO.PaymentDate,
                PaymentStatus = paymentDTO.PaymentStatus,
            };

            await _unitOfWork.PaymentRepository.Add(payment);
            await _unitOfWork.Complete();

            return CreatedAtAction(nameof(GetPayment), new { id = payment.Id }, payment);
        }



        // PUT: api/payments/Edit/5
        [HttpPut("Edit/{id:int}")]
        public async Task<IActionResult> PutPayment(int id, PaymentDTO paymentDTO)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var payment = await _unitOfWork.PaymentRepository.GetById(id);


            payment.TenantId = paymentDTO.TenantId;
            payment.LeaseId = paymentDTO.LeaseId;
            payment.PaymentAmount = paymentDTO.PaymentAmount;
            payment.PaymentDate = paymentDTO.PaymentDate;
            payment.PaymentStatus = paymentDTO.PaymentStatus;
            
            _unitOfWork.PaymentRepository.Update(payment);

            try
            {
                await _unitOfWork.Complete();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await PaymentExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }



        // DELETE: api/payments/Delete/5
        [HttpDelete("Delete/{id:int}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            var payment = await _unitOfWork.PaymentRepository.GetById(id);
            if (payment == null)
            {
                return NotFound();
            }

            _unitOfWork.PaymentRepository.Delete(payment);
            await _unitOfWork.Complete();

            return NoContent();
        }

        private async Task<bool> PaymentExists(int id)
        {
            var payment = await _unitOfWork.PaymentRepository.GetById(id);

            if (payment == null)
            {
                return false;
            }
                

            return true;
        }


    }
}
