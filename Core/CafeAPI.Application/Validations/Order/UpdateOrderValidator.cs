using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CafeAPI.Application.Dtos.OrderDto;
using FluentValidation;

namespace CafeAPI.Application.Validations.Order
{
    public class UpdateOrderValidator:AbstractValidator<UpdateOrderDto>
    {
        public UpdateOrderValidator()
        {
        //    RuleFor(x => x.TotalPrice)
        //            .NotEmpty().WithMessage("Toplam fiyat boş olamaz")
        //            .GreaterThan(0).WithMessage("Sipariş ücreti 0'dan büyük olmalıdır.");
        }
    }
}
