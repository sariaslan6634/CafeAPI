using CafeAPI.Application.Dtos.ResponseDto;
using CafeAPI.Application.Dtos.TableDto;
using CafeAPI.Application.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CafeAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TablesController : ControllerBase
    {
        private readonly ITableServices _tableServices;
        public TablesController(ITableServices tableServices)
        {
            _tableServices = tableServices;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllTable()
        {
            var result = await _tableServices.GetAllTables();
            if (!result.Success)
            {
                if (result.ErrorCodes == ErrorCodes.NotFound)
                    return Ok(result);
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpGet("GetAllIsActiveTablesGeneric")]
        public async Task<IActionResult> GetAllIsActiveTablesGeneric()
        {
            var result = await _tableServices.GetAllActiveTablesGeneric();
            if (!result.Success)
            {
                if (result.ErrorCodes == ErrorCodes.NotFound)
                    return Ok(result);
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpGet("GetAllIsActiveTables")]
        public async Task<IActionResult> GetAllIsActiveTables()
        {
            var result = await _tableServices.GetAllActiveTables();
            if (!result.Success)
            {
                if (result.ErrorCodes == ErrorCodes.NotFound)
                    return Ok(result);
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdTable(int id)
        {
            var result = await _tableServices.GetByIdTable(id);
            if (!result.Success)
            {
                if (result.ErrorCodes == ErrorCodes.NotFound)
                    return Ok(result);
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpGet("GetByTableNumber")]
        public async Task<IActionResult> GetByTableNumber(int tableNumber)
        {
            var result = await _tableServices.GetByTableNumber(tableNumber);
            if (!result.Success)
            { 
                if(result.ErrorCodes == ErrorCodes.NotFound)
                    return Ok(result);
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> AddTables(CreateTableDto dto)
        {
            var result = await _tableServices.AddTable(dto);
            if (!result.Success)
            {
                if (result.ErrorCodes is ErrorCodes.ValidationError or ErrorCodes.DuplicateError)
                {
                    return Ok(result);
                }
                BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateTable(UpdateTableDto dto)
        { 
            var result = await _tableServices.UpdateTable(dto);
            if (!result.Success)
            {
                if (result.ErrorCodes is ErrorCodes.ValidationError or ErrorCodes.NotFound)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPut("UpdateTableStatusById")]
        public async Task<IActionResult> UpdateTableStatusById(int id)
        {
            var result = await _tableServices.UpdateTableStatusById(id);
            if (!result.Success)
            {
                if (result.ErrorCodes is ErrorCodes.ValidationError or ErrorCodes.NotFound)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPut("UpdateTableStatusByTableNumber")]
        public async Task<IActionResult> UpdateTableStatusByTableNumber(int tableNumber)
        {
            var result = await _tableServices.UpdateTablStatusByTableNumber(tableNumber);
            if (!result.Success)
            {
                if (result.ErrorCodes is ErrorCodes.ValidationError or ErrorCodes.NotFound)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteTable(int id)
        {
            var result = await _tableServices.DeleteTable(id);
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
    }
}
