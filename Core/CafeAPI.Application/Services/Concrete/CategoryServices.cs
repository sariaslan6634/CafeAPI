using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CafeAPI.Application.Dtos.CategoryDto;
using CafeAPI.Application.Interfaces;
using CafeAPI.Application.Services.Abstract;
using CafeAPI.Domain.Entities;

namespace CafeAPI.Application.Services.Concrete
{
    public class CategoryServices : ICategoryServices
    {
        private readonly IGenericRepository<Category> _categoryRepository;

        public CategoryServices(IGenericRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public Task AddCategory(CreateCategoryDto dto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCategory(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ResultCategoryDto>> GetAllCategories()
        {
            throw new NotImplementedException();
        }

        public Task<DetailCategoryDto> GetByIdCategory(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCategory(UpdateCategoryDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
