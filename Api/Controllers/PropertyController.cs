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
public class PropertyController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWebHostEnvironment webHostEnvironment;
    private readonly UserManager<ApplicationUser> _userManager;

    public PropertyController
        (IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment,UserManager<ApplicationUser> userManager)
    {
        this._unitOfWork = unitOfWork;
        this.webHostEnvironment = webHostEnvironment;
        this._userManager = userManager;
    }
    //111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111
    #region GetAll
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll([FromQuery] string? search)
    {
        if (string.IsNullOrEmpty(search))
        {
            var allProperties = await _unitOfWork.PropertyRepository.GetAll();
            return Ok(allProperties);
        }
        else
        {
            var properties = (await _unitOfWork.PropertyRepository.GetAll())
                .Where(e => e.Name.ToLower().Contains(search.ToLower())
                         || e.Address.ToLower().Contains(search.ToLower())
                         || e.Description.ToLower().Contains(search.ToLower()));

            return Ok(properties);
        }
    }
    #endregion
    //111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111
    #region GetAllForManager
    [HttpGet("GetAllForManager")]
    [Authorize(Roles =SD.Manager)]
    public async Task<IActionResult> GetAllForManager()
    {
        var user=await _userManager.Users.FirstOrDefaultAsync();
        return Ok((await _unitOfWork.PropertyRepository.GetAll()).Where(e=>e.OwnerId==user.Id));
    }
    #endregion
    //111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111
    #region GetAllForTenant
    [HttpGet("GetAllForTenant")]
    [Authorize(Roles = SD.Tenant)]
    public async Task<IActionResult> GetAllForTenant()
    {
        var user = await _userManager.Users.FirstOrDefaultAsync();
        return Ok((await _unitOfWork.PropertyRepository.GetAll()).Where(e => e.OwnerId == user.Id));
    }
    #endregion
    //111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111
    #region GetById
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

        var user=await _userManager.Users.FirstOrDefaultAsync();

        Property property = new()
        {
            Name = propertyDTO.Name,
            Img = propertyDTO.Img,
            Description = propertyDTO.Description,
            Address = propertyDTO.Address,
            RentPrice = propertyDTO.RentPrice,
            SquareFootage = propertyDTO.SquareFootage,
            AvailabilityStatus = propertyDTO.AvailabilityStatus,
            PropertyType = propertyDTO.PropertyType,
            OwnerId = user.Id
        };

        await _unitOfWork.PropertyRepository.Add(property);

        await _unitOfWork.Complete();
        return Ok();
    }
    #endregion
    //111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111
    #region Edit
    [HttpPut("Edit/{id:int}")]
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
        property.Img = propertyDTO.Img;
        property.Address = propertyDTO.Address;
        property.Description= propertyDTO.Description;
        property.RentPrice = propertyDTO.RentPrice;
        property.SquareFootage = propertyDTO.SquareFootage;
        property.AvailabilityStatus = propertyDTO.AvailabilityStatus;
        property.PropertyType = propertyDTO.PropertyType;

        _unitOfWork.PropertyRepository.Update(property);

        await _unitOfWork.Complete();

        return Ok();
    }
    #endregion
    //111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111
    #region Delete
    [HttpDelete("Delete/{id:int}")]
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
