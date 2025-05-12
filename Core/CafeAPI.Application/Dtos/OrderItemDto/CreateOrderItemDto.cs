using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CafeAPI.Application.Dtos.MenuItemDto;
using CafeAPI.Domain.Entities;

namespace CafeAPI.Application.Dtos.OrderItemDto
{
    public class CreateOrderItemDto
    {
        public int OrderId { get; set; }
        public int MenuItemId { get; set; }
        public int Quantity { get; set; }
    }
}
