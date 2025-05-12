using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CafeAPI.Application.Dtos.OrderItemDto;
using FluentValidation;

namespace CafeAPI.Application.Validations.OrderItem
{
    public class UpdateOrderItemValidator:AbstractValidator<UpdateOrderItemDto>
    {
        public UpdateOrderItemValidator()
        {
            RuleFor(x => x.Quantity)
                .NotEmpty().WithMessage("Sipariş adeti boş olamaz")
                .GreaterThan(0).WithMessage("Sipariş adeti 0'dan büyük olmak zorunda.");
        }
    }
}
