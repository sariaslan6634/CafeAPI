using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CafeAPI.Application.Dtos.OrderItemDto;

namespace CafeAPI.Application.Dtos.OrderDto
{
    public class AddOrderItemByOrderDto
    {
        public int OrderId { get; set; }
        public CreateOrderItemDto OrderItem { get; set; }
    }
}
