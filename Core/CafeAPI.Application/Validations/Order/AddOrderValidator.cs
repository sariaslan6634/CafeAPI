﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CafeAPI.Application.Dtos.OrderDto;
using FluentValidation;

namespace CafeAPI.Application.Validations.Order
{
    public class AddOrderValidator:AbstractValidator<CreateOrderDto>
    {
        public AddOrderValidator()
        {
            //RuleFor(x => x.)
            //    .NotEmpty().WithMessage("Toplam fiyat boş olamaz")
            //    .GreaterThan(0).WithMessage("Sipariş ücreti 0'dan büyük olmalıdır.");
        }
    }
}
