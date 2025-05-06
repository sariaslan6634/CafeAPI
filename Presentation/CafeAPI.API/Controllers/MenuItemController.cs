using CafeAPI.Application.Dtos.MenuItemDto;
using CafeAPI.Application.Dtos.ResponseDto;
using CafeAPI.Application.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CafeAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemController : ControllerBase
    {
        private readonly IMenuItemServices _menuItemServices;

        public MenuItemController(IMenuItemServices menuItemServices)
        {
            _menuItemServices = menuItemServices;
        }
        [HttpGet]
        public async Task<ActionResult> GetAllMenuItems() 
        {
            var result = await _menuItemServices.GetAllMenuItems();
            if (!result.Success)
            {
                if (result.ErrorCodes == ErrorCodes.NotFound)
                    return Ok(result);
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdMenuItem(int id)
        {
            var result = await _menuItemServices.GetByIdMenuItem(id);
            if (!result.Success)
            {
                if (result.ErrorCodes == ErrorCodes.NotFound)
                    return Ok(result);
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateMenuItems(CreateMenuItemDto dto)
        {
            var result = await _menuItemServices.AddManuItem(dto);
            if (!result.Success)
            {
                if(result.ErrorCodes is ErrorCodes.ValidationError or ErrorCodes.NotFound)
                    return Ok(result);
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateMenuItem(UpdateMenuItemDto dto)
        {
            
            var result = await _menuItemServices.UpdateManuItem(dto);
            if (!result.Success)
            {
                if (result.ErrorCodes is ErrorCodes.NotFound or ErrorCodes.ValidationError)
                    return Ok(result);
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteMenuItem(int id)
        {
            var result =await _menuItemServices.DeleteManuItem(id);
            if (!result.Success)
            {
                if (result.ErrorCodes ==ErrorCodes.NotFound)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
