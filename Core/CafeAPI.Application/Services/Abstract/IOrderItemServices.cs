using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CafeAPI.Application.Dtos.OrderItemDto;
using CafeAPI.Application.Dtos.ResponseDto;

namespace CafeAPI.Application.Services.Abstract
{
    public interface IOrderItemServices
    {
        Task<ResponseDto<List<ResultOrderItemDto>>> GetAllOrderItems();
        Task<ResponseDto<DetailOrderItemDto>> GetOrderItemById(int id);
        Task<ResponseDto<object>> AddOrderItem(CreateOrderItemDto dto);
        Task<ResponseDto<object>> UpdetOrderItem(UpdateOrderItemDto dto);
        Task<ResponseDto<object>> DeleteOrderItem(int dto);
    }
}
