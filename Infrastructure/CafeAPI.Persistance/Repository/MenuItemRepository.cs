using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CafeAPI.Application.Interfaces;
using CafeAPI.Domain.Entities;
using CafeAPI.Persistance.Context;
using Microsoft.EntityFrameworkCore;

namespace CafeAPI.Persistance.Repository
{
    public class MenuItemRepository : IMenuItemRepository
    {
        private readonly AppDbContext _context;

        public MenuItemRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<MenuItem>> GetMenuItemFilterByCategoryId(int categoryId)
        {
            var result = await _context.MenuItems.Where(x => x.CategoryId == categoryId).ToListAsync();
            return result;
        }
    }
}
