using System.Threading.Tasks;
using CafeAPI.Application.Dtos.OrderDto;
using CafeAPI.Application.Services.Abstract;
using CafeAPI.Application.Services.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CafeAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : BaseController
    {
        private readonly IOrderServices _orderServices;
        public OrdersController(IOrderServices orderServices)
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
        [HttpGet("GetAllOrderWithDetail")]
        public async Task<IActionResult> GetAllOrderWithDetail()
        {
            var result = await _orderServices.GettAllOrdersWithDetail();
            return CreateResponse(result);

        }
        [HttpPut("UpdateOrderStatusHazir")]
        public async Task<IActionResult> UpdateOrderStatusHazir(int id)
        {
            var result = await _orderServices.UpdateOrderStatusHazir(id);
            return CreateResponse(result);
        }
        [HttpPut("UpdateOrderStatusIptalEdildi")]
        public async Task<IActionResult> UpdateOrderStatusIptalEdildi(int id)
        {
            var result = await _orderServices.UpdateOrderStatusIptalEdildi(id);
            return CreateResponse(result);
        }
        [HttpPut("UpdateOrderStatusTeslimEdildi")]
        public async Task<IActionResult> UpdateOrderStatusTeslimEdildi(int id)
        {
            var result = await _orderServices.UpdateOrderStatusTeslimEdildi(id);
            return CreateResponse(result);
        }
        //[HttpPut("AddOrderItemByOrder")]
        //public async Task<IActionResult> AddOrderItemByOrder(AddOrderItemByOrderDto dto)
        //{
        //    var result = await _orderServices.AddOrderItemByOrderId(dto);
        //    return CreateResponse(result);
        //}
    }
}
