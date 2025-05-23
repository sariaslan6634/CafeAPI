﻿using CafeAPI.Application.Dtos.ResponseDto;
using CafeAPI.Application.Dtos.TableDto;
using CafeAPI.Application.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CafeAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TablesController : BaseController
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
            return CreateResponse(result);
        }
        [HttpGet("GetAllIsActiveTablesGeneric")]
        public async Task<IActionResult> GetAllIsActiveTablesGeneric()
        {
            var result = await _tableServices.GetAllActiveTablesGeneric();
            return CreateResponse(result);
        }
        [HttpGet("GetAllIsActiveTables")]
        public async Task<IActionResult> GetAllIsActiveTables()
        {
            var result = await _tableServices.GetAllActiveTables();
            return CreateResponse(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdTable(int id)
        {
            var result = await _tableServices.GetByIdTable(id);
            return CreateResponse(result);
        }
        [HttpGet("GetByTableNumber")]
        public async Task<IActionResult> GetByTableNumber(int tableNumber)
        {
            var result = await _tableServices.GetByTableNumber(tableNumber);
            return CreateResponse(result);
        }
        [HttpPost]
        public async Task<IActionResult> AddTables(CreateTableDto dto)
        {
            var result = await _tableServices.AddTable(dto);
            return CreateResponse(result);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateTable(UpdateTableDto dto)
        { 
            var result = await _tableServices.UpdateTable(dto);
            return CreateResponse(result);
        }
        [HttpPut("UpdateTableStatusById")]
        public async Task<IActionResult> UpdateTableStatusById(int id)
        {
            var result = await _tableServices.UpdateTableStatusById(id);
            return CreateResponse(result);
        }
        [HttpPut("UpdateTableStatusByTableNumber")]
        public async Task<IActionResult> UpdateTableStatusByTableNumber(int tableNumber)
        {
            var result = await _tableServices.UpdateTablStatusByTableNumber(tableNumber);
            return CreateResponse(result);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteTable(int id)
        {
            var result = await _tableServices.DeleteTable(id);
            return CreateResponse(result);
        }
    }
}
