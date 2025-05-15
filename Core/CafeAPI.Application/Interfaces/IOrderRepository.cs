using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CafeAPI.Domain.Entities;

namespace CafeAPI.Application.Interfaces
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAllOrderWithDetailAsync();
        Task<Order> GetOrderByIdWithDetailAsync(int orderId);
    }
}
