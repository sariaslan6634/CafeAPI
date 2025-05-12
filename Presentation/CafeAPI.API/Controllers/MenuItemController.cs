using CafeAPI.Application.Dtos.MenuItemDto;
using CafeAPI.Application.Dtos.ResponseDto;
using CafeAPI.Application.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CafeAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemController : BaseController
    {
        private readonly IMenuItemServices _menuItemServices;

        public MenuItemController(IMenuItemServices menuItemServices)
        {
            _menuItemServices = menuItemServices;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllMenuItems() 
        {
            var result = await _menuItemServices.GetAllMenuItems();
            return CreateResponse(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdMenuItem(int id)
        {
            var result = await _menuItemServices.GetByIdMenuItem(id);
            return CreateResponse(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateMenuItems(CreateMenuItemDto dto)
        {
            var result = await _menuItemServices.AddManuItem(dto);
            return CreateResponse(result);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateMenuItem(UpdateMenuItemDto dto)
        {
            
            var result = await _menuItemServices.UpdateManuItem(dto);
            return CreateResponse(result);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteMenuItem(int id)
        {
            var result =await _menuItemServices.DeleteManuItem(id);
            return CreateResponse(result);
        }
    }
}
