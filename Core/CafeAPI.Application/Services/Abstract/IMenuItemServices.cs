using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CafeAPI.Application.Dtos.MenuItemDto;

namespace CafeAPI.Application.Services.Abstract
{
    public interface IMenuItemServices
    {
        Task<List<ResultMenuItemDto>> GetAllMenuItems();
        Task<DetailMenuItemDto> GetByIdMenuItem(int id);
        Task AddManuItem(CreateMenuItemDto dto);
        Task UpdateManuItem(UpdateMenuItemDto dto);
        Task DeleteManuItem(int id);    
    }
}
