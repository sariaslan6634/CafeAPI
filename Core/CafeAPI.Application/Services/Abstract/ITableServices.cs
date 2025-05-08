using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CafeAPI.Application.Dtos.ResponseDto;
using CafeAPI.Application.Dtos.TableDto;

namespace CafeAPI.Application.Services.Abstract
{
    public interface ITableServices
    {
        Task<ResponseDto<List<ResultTableDto>>> GetAllTables();
        Task<ResponseDto<DetailTableDto>> GetByIdTable(int id);
        Task<ResponseDto<DetailTableDto>> GetByTableNumber(int tabloNumber);
        Task<ResponseDto<object>> AddTable(CreateTableDto dto);
        Task<ResponseDto<object>> UpdateTable(UpdateTableDto dto);
        Task<ResponseDto<object>> DeleteTable(int id);
    }
}
