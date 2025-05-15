﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CafeAPI.Application.Dtos.OrderItemDto;

namespace CafeAPI.Application.Dtos.OrderDto
{
    public class CreateOrderDto
    {
        public int TableId { get; set; }
        //public decimal TotalPrice { get; set; }
        //public DateTime CreatedAt { get; set; } = DateTime.Now;
        //public DateTime? UpdateAt { get; set; }
        //public string Status { get; set; }
        public List<CreateOrderItemDto> OrderItems { get; set; }
    }
}
