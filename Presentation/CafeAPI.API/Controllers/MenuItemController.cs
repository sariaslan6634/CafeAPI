using CafeAPI.Application.Dtos.MenuItemDto;
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
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdMenuItem(int id)
        {
            var result = await _menuItemServices.GetByIdMenuItem(id);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateMenuItems(CreateMenuItemDto dto)
        {
            await _menuItemServices.AddManuItem(dto);
            return Ok("Menü item eklendi.");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateMenuItem(UpdateMenuItemDto dto)
        {
            await _menuItemServices.UpdateManuItem(dto);
            return Ok("Menü item güncellendi!");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteMenuItem(int id)
        {
            await _menuItemServices.DeleteManuItem(id);
            return Ok("Menü item silindi!");
        }
    }
}
