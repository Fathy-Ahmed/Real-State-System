using BL.DTO;
using BL.Interfaces;
using BL.Utilities;
using DL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment webHostEnvironment;

        public PropertyController
            (IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            this._unitOfWork = unitOfWork;
            this.webHostEnvironment = webHostEnvironment;
        }
        //111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111
        #region GetAll
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _unitOfWork.PropertyRepository.GetAll());
        }
        #endregion
        //111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111
        #region Get
        [HttpGet("Get/{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            Property property = await _unitOfWork.PropertyRepository.GetById(id);
            if(property == null)
                return NotFound("Property not found");

            return Ok(property);
        }
        #endregion
        //111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111
        #region Add
        [HttpPost("Add")]
        [Authorize(Roles =SD.Manager)]
        public async Task<IActionResult> Add(PropertyDTO propertyDTO)
        {
            if(!ModelState.IsValid) 
                return BadRequest(ModelState);


            // Photo File
            if (propertyDTO.file != null && propertyDTO.file.Length > 0)
            {
                string RootPath = webHostEnvironment.WebRootPath;
                string filename = Guid.NewGuid().ToString();
                var Upload = Path.Combine(RootPath, @"images\Property");
                var extension = Path.GetExtension(propertyDTO.file.FileName);
                if (propertyDTO.Img != null)
                {
                    var OldImg = Path.Combine(RootPath, propertyDTO.Img.TrimStart('\\'));
                    if (System.IO.File.Exists(OldImg))
                    {
                        System.IO.File.Delete(OldImg);
                    }
                }

                using (var fileStream = new FileStream(Path.Combine(Upload, filename + extension), FileMode.Create))
                {
                    await propertyDTO.file.CopyToAsync(fileStream);
                }
                propertyDTO.Img = @"images\Property\" + filename + extension;
            }

            Property property = new()
            {
                Name = propertyDTO.Name,
                Description = propertyDTO.Description,
                Img = propertyDTO.Img,
                Location = propertyDTO.Location,
                Price = propertyDTO.Price,
                Size = propertyDTO.Size,
            };

            await _unitOfWork.PropertyRepository.Add(property);

            await _unitOfWork.Complete();
            return Ok();
        }
        #endregion
        //111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111
        #region Edit
        [HttpPost("Edit/{id:int}")]
        [Authorize(Roles = SD.Manager)]
        public async Task<IActionResult> Edit(PropertyDTO propertyDTO,int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            Property property = await _unitOfWork.PropertyRepository.GetById(id);

            if (property == null)
                return BadRequest("property not found");

            // Photo File
            if (propertyDTO.file != null && propertyDTO.file.Length > 0)
            {
                string RootPath = webHostEnvironment.WebRootPath;
                string filename = Guid.NewGuid().ToString();
                var Upload = Path.Combine(RootPath, @"images\Doctors");
                var extension = Path.GetExtension(propertyDTO.file.FileName);
                if (propertyDTO.Img != null)
                {
                    var OldImg = Path.Combine(RootPath, propertyDTO.Img.TrimStart('\\'));
                    if (System.IO.File.Exists(OldImg))
                    {
                        System.IO.File.Delete(OldImg);
                    }
                }

                using (var fileStream = new FileStream(Path.Combine(Upload, filename + extension), FileMode.Create))
                {
                    await propertyDTO.file.CopyToAsync(fileStream);
                }
                propertyDTO.Img = @"images\Doctors\" + filename + extension;
            }


            property.Name = propertyDTO.Name;
            property.Description = propertyDTO.Description;
            property.Img = propertyDTO.Img;
            property.Location = propertyDTO.Location;
            property.Price = propertyDTO.Price;
            property.Size = propertyDTO.Size;
            

            _unitOfWork.PropertyRepository.Update(property);
            await _unitOfWork.Complete();

            return Ok();
        }
        #endregion
        //111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111
        #region Delete
        [HttpPost("Delete/{id:int}")]
        [Authorize(Roles = SD.Manager)]
        public async Task<IActionResult> Delete(int id)
        {
            Property property = await _unitOfWork.PropertyRepository.GetById(id);
            if(property == null) 
                return BadRequest("property not found");

            _unitOfWork.PropertyRepository.Delete(property);
            await _unitOfWork.Complete();

            return Ok();
        }
        #endregion
        //111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111
    }
}
