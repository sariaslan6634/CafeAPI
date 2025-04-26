using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CafeAPI.Application.Dtos.MenuItemDto;
using CafeAPI.Application.Interfaces;
using CafeAPI.Application.Services.Abstract;
using CafeAPI.Domain.Entities;

namespace CafeAPI.Application.Services.Concrete
{
    public class MenuItemServices : IMenuItemServices
    {
        private readonly IGenericRepository<MenuItem> _menuItemsRepository;

        public MenuItemServices(IGenericRepository<MenuItem> menuItemsRepository)
        {
            _menuItemsRepository = menuItemsRepository;
        }

        public Task AddManuItem(CreateMenuItemDto dto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteManuItem(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ResultMenuItemDto>> GetAllMenuItems()
        {
            throw new NotImplementedException();
        }

        public Task<DetailMenuItemDto> GetByIdMenuItem(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateManuItem(UpdateMenuItemDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
