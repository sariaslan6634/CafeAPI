using System.Threading.Tasks;
using CafeAPI.Application.Dtos.CategoryDto;
using CafeAPI.Application.Dtos.ResponseDto;
using CafeAPI.Application.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CafeAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryServices _categoryServices;
        public CategoriesController(ICategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var result = await _categoryServices.GetAllCategories();
            if (!result.Success)
            {
                if (result.ErrorCodes == ErrorCodes.NotFound)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetbyIdCategory(int id)
        {
            var result = await _categoryServices.GetByIdCategory(id);
            if (!result.Success)
            {
                if (result.ErrorCodes == ErrorCodes.NotFound)
                    return Ok(result);
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> AddCategory(CreateCategoryDto dto)
        {
            var result = await _categoryServices.AddCategory(dto);
            if(!result.Success)
                if (result.ErrorCodes == ErrorCodes.ValidationError)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto dto)
        {
            var result = await _categoryServices.UpdateCategory(dto);
            if(!result.Success)
            {
                if (result.ErrorCodes is ErrorCodes.ValidationError or ErrorCodes.NotFound)
                {
                    return Ok(result);
                }
                if(result.ErrorCodes == ErrorCodes.NotFound)
                {                    
                    return Ok(result);
                }           
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var result = await _categoryServices.DeleteCategory(id);
            if (!result.Success)
            {
                if(result.ErrorCodes == ErrorCodes.NotFound)
                    return Ok(result);
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
