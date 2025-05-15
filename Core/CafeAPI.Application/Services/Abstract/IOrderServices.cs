using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CafeAPI.Application.Dtos.OrderDto;
using CafeAPI.Application.Dtos.OrderItemDto;
using CafeAPI.Application.Dtos.ResponseDto;

namespace CafeAPI.Application.Services.Abstract
{
    public interface IOrderServices
    {
        Task<ResponseDto<List<ResultOrderDto>>> GetAllOrder();
        Task<ResponseDto<DetailOrderDto>> GetOrderById(int id);
        Task<ResponseDto<object>> AddOrder(CreateOrderDto dto);
        Task<ResponseDto<object>> UpdateOrder(UpdateOrderDto dto);
        Task<ResponseDto<object>> DeleteOrder(int id);
        Task<ResponseDto<List<ResultOrderDto>>> GettAllOrdersWithDetail();
        Task<ResponseDto<object>> UpdateOrderStatusHazir(int orderId);
        Task<ResponseDto<object>> UpdateOrderStatusTeslimEdildi(int orderId);
        Task<ResponseDto<object>> UpdateOrderStatusIptalEdildi(int orderId);

       //Task<ResponseDto<object>> AddOrderItemByOrderId(AddOrderItemByOrderDto dto);
    }
}
