using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CafeAPI.Application.Dtos.TableDto;
using FluentValidation;

namespace CafeAPI.Application.Validations.Table
{
    public class UpdateTableValidator:AbstractValidator<UpdateTableDto>
    {
        public UpdateTableValidator()
        {
            RuleFor(x => x.TableNumber)
                .NotEmpty().WithMessage("Masa numarası boş olamaz.")
                .GreaterThan(0).WithMessage("Masa numarası 0'dan büyük olmalıdır.");
            RuleFor(x => x.Capacity)
                .NotEmpty().WithMessage("Kapasite boş olamaz.")
                .GreaterThan(0).WithMessage("Kapasite 0'dan büyük olmak zorundadır.");
        }
    }
}
