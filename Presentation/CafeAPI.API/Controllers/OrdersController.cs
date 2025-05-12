using System.Threading.Tasks;
using CafeAPI.Application.Dtos.OrderDto;
using CafeAPI.Application.Services.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CafeAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : BaseController
    {
        private readonly OrderServices _orderServices;
        public OrdersController(OrderServices orderServices)
        {
            _orderServices = orderServices;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var result = await _orderServices.GetAllOrder();
            return CreateResponse(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var result = await _orderServices.GetOrderById(id);
            return CreateResponse(result);
        }
        [HttpPost]
        public async Task<IActionResult> AddOrder(CreateOrderDto dto)
        {
            var result = await _orderServices.AddOrder(dto);
            return CreateResponse(result);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateOrder(UpdateOrderDto dto)
        {
            var result = await _orderServices.UpdateOrder(dto);
            return CreateResponse(result);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var result = await _orderServices.DeleteOrder(id);
            return CreateResponse(result);
        }
    }
}
