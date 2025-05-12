using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CafeAPI.Application.Dtos.OrderItemDto;
using CafeAPI.Domain.Entities;

namespace CafeAPI.Application.Dtos.OrderDto
{
    public class DetailOrderDto
    {
        public int Id { get; set; }
        public int TableId { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public string Status { get; set; }
        public List<ResultOrderItemDto> OrderItems { get; set; }
    }
}
