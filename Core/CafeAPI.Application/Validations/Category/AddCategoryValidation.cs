using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CafeAPI.Application.Dtos.CategoryDto;
using FluentValidation;

namespace CafeAPI.Application.Validations.Category
{
    public class AddCategoryValidation : AbstractValidator<CreateCategoryDto>
    {
        public AddCategoryValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Kategori boş bırakılamaz.")
                .Length(3, 30).WithMessage("Kategori adının uzunluğu 3 ile 30 arasında olması gerekmektedir.");
        }
    }
}
