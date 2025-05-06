using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CafeAPI.Application.Dtos.MenuItemDto;
using FluentValidation;

namespace CafeAPI.Application.Validations.MenuItem
{
    public class AddMenuItemValidator:AbstractValidator<CreateMenuItemDto>
    {
        public AddMenuItemValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("menu item ismi boş olamaz")
                .Length(2, 40).WithMessage("Menu item adı 2 ile 40 karekter arasında olmak zorundadır.");
            RuleFor(x=>x.Description)
                .NotEmpty().WithMessage("menu item açıklaması boş olamaz")
                .Length(5,100).WithMessage("Menu item açıklaması 5 ile 100 karekter arasında olmak zorundadır.");
            RuleFor(x => x.Price)
                .NotEmpty().WithMessage("menu item fiyatı boş olamaz")
                .GreaterThan(0).WithMessage("Menu item fiyatı 0 dan büyük olmak zorundadır.");
            RuleFor(x => x.ImageUrl)
                .NotEmpty().WithMessage("Menu item fotoğraf url'i boş olamaz.");
        }
    }
}
