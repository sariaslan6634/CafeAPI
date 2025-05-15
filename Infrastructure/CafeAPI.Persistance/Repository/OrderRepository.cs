using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CafeAPI.Application.Interfaces;
using CafeAPI.Domain.Entities;
using CafeAPI.Persistance.Context;
using Microsoft.EntityFrameworkCore;

namespace CafeAPI.Persistance.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _contex;
        public OrderRepository(AppDbContext context)
        {
            _contex = context;
        }

        public async Task<List<Order>> GetAllOrderWithDetailAsync()
        {
            var result = await _contex.Order.Include(x => x.OrderItems).ThenInclude(x => x.MenuItem).ThenInclude(x=>x.Category).ToListAsync();
            return result;
        }
        public async Task<Order> GetOrderByIdWithDetailAsync(int orderId)
        {
            var result = await _contex.Order.Include(x => x.OrderItems).ThenInclude(x => x.MenuItem).ThenInclude(x => x.Category).Where(x => x.Id == orderId).FirstOrDefaultAsync();
            return result;
        }
    }
}
