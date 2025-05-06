using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CafeAPI.Application.Dtos.MenuItemDto;
using CafeAPI.Application.Dtos.ResponseDto;

namespace CafeAPI.Application.Services.Abstract
{
    public interface IMenuItemServices
    {
        Task<ResponseDto<List<ResultMenuItemDto>>> GetAllMenuItems();
        Task<ResponseDto<DetailMenuItemDto>> GetByIdMenuItem(int id);
        Task<ResponseDto<object>> AddManuItem(CreateMenuItemDto dto);
        Task<ResponseDto<object>> UpdateManuItem(UpdateMenuItemDto dto);
        Task<ResponseDto<object>> DeleteManuItem(int id);    
    }
}
