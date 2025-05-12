using System.Threading.Tasks;
using CafeAPI.Application.Dtos.OrderDto;
using CafeAPI.Application.Dtos.OrderItemDto;
using CafeAPI.Application.Services.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CafeAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemsController : BaseController
    {
        private readonly OrderItemServices _orderItemServices;

        public OrderItemsController(OrderItemServices orderItemServices)
        {
            _orderItemServices = orderItemServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrderItems()
        {
            var result = await _orderItemServices.GetAllOrderItems();
            return CreateResponse(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderItemById(int id)
        {
            var result = await _orderItemServices.GetOrderItemById(id);
            return CreateResponse(result);
        }
        [HttpPost]
        public async Task<IActionResult> AddOrderItem(CreateOrderItemDto dto)
        {
            var result = await _orderItemServices.AddOrderItem(dto);
            return CreateResponse(result);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateOrderItem(UpdateOrderItemDto dto)
        {
            var result = await _orderItemServices.UpdetOrderItem(dto);
            return CreateResponse(result);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteOrderItem(int id)
        {
            var result = await _orderItemServices.DeleteOrderItem(id);
            return CreateResponse(result);
        }
    }
}
