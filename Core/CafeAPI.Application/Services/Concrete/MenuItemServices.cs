using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CafeAPI.Application.Dtos.MenuItemDto;
using CafeAPI.Application.Interfaces;
using CafeAPI.Application.Services.Abstract;
using CafeAPI.Domain.Entities;

namespace CafeAPI.Application.Services.Concrete
{
    public class MenuItemServices : IMenuItemServices
    {
        private readonly IGenericRepository<MenuItem> _menuItemsRepository;
        private readonly IMapper _mapper;

        public MenuItemServices(IGenericRepository<MenuItem> menuItemsRepository, IMapper mapper)
        {
            _menuItemsRepository = menuItemsRepository;
            _mapper = mapper;
        }

        public async Task AddManuItem(CreateMenuItemDto dto)
        {
            var menuItem = _mapper.Map<MenuItem>(dto);
            await _menuItemsRepository.AddAsync(menuItem);
        }

        public async Task DeleteManuItem(int id)
        {
            var menuItem = await _menuItemsRepository.GetByIdAsync(id);
            await _menuItemsRepository.DeleteAsync(menuItem);
        }

        public async Task<List<ResultMenuItemDto>> GetAllMenuItems()
        {
            var menuItems = await _menuItemsRepository.GetAllAsync();
            var result = _mapper.Map<List<ResultMenuItemDto>>(menuItems);
            return result;
        }

        public async Task<DetailMenuItemDto> GetByIdMenuItem(int id)
        {
            var menuItem = await _menuItemsRepository.GetByIdAsync(id);
            var result = _mapper.Map<DetailMenuItemDto>(menuItem);
            return result; 
        }

        public async Task UpdateManuItem(UpdateMenuItemDto dto)
        {
            var menuItem = _mapper.Map<MenuItem>(dto);
            await _menuItemsRepository.UpdateAsync(menuItem);
        }
    }
}
