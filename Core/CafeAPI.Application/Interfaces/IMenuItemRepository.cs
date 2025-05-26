using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CafeAPI.Application.Dtos.MenuItemDto;
using CafeAPI.Domain.Entities;

namespace CafeAPI.Application.Interfaces
{
    public interface IMenuItemRepository
    {
        Task<List<MenuItem>> GetMenuItemFilterByCategoryId(int categoryId); 
    }
}
