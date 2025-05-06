using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CafeAPI.Application.Dtos.CategoryDto;
using CafeAPI.Application.Dtos.ResponseDto;

namespace CafeAPI.Application.Services.Abstract
{
    public interface ICategoryServices
    {
        Task<ResponseDto<List<ResultCategoryDto>>> GetAllCategories();
        Task<ResponseDto<DetailCategoryDto>> GetByIdCategory(int id);
        Task <ResponseDto<object>> AddCategory(CreateCategoryDto dto);
        Task <ResponseDto<object>> UpdateCategory(UpdateCategoryDto dto);
        Task<ResponseDto<object>> DeleteCategory(int id);
    }
}
