using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CafeAPI.Application.Dtos.MenuItemDto;

namespace CafeAPI.Application.Dtos.CategoryDto
{
    public class DetailCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ResultMenuItemDto> MenuItems { get; set; }
    }
}
