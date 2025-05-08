using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CafeAPI.Application.Dtos.ResponseDto;
using CafeAPI.Application.Dtos.TableDto;
using CafeAPI.Application.Interfaces;
using CafeAPI.Application.Services.Abstract;
using CafeAPI.Domain.Entities;
using FluentValidation;

namespace CafeAPI.Application.Services.Concrete
{
    public class TableServices:ITableServices
    {
        private readonly IGenericRepository<Table> _tableRepository;
        private readonly ITableRepository _tableRepository1;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateTableDto> _createTableValidator;
        private readonly IValidator<UpdateTableDto> _updateTableValidator;


        public TableServices(IGenericRepository<Table> tableRepository, IMapper mapper, IValidator<CreateTableDto> createTableValidator, IValidator<UpdateTableDto> updateTableValidator, ITableRepository tableRepository1)
        {
            _tableRepository = tableRepository;
            _mapper = mapper;
            _createTableValidator = createTableValidator;
            _updateTableValidator = updateTableValidator;
            _tableRepository1 = tableRepository1;
        }

        public async Task<ResponseDto<object>> AddTable(CreateTableDto dto)
        {
            try
            {
                var validator = await _createTableValidator.ValidateAsync(dto);
                if (!validator.IsValid)
                {
                    return new ResponseDto<object> 
                    {
                        Success = false,
                        Data = null,
                        Message = string.Join(" | ",validator.Errors.Select(x=>x.ErrorMessage)),
                        ErrorCodes = ErrorCodes.ValidationError
                    };
                }
                var checkTable = await _tableRepository1.GetByTableNumberAsync(dto.TableNumber);
                if (checkTable != null)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Data = null,
                        Message = "Eklemek istediğiniz masa numarası mevcuttur.",
                        ErrorCodes = ErrorCodes.DuplicateError
                    };
                }
                var result = _mapper.Map<Table>(dto);
                await _tableRepository.AddAsync(result);
                return new ResponseDto<object>
                {
                    Success = true,
                    Data = result,
                    Message = "Masa başarılı bir şekilde eklendi."
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto<object>
                {
                    Success = false,
                    Data = null,
                    Message = "Bir hata oluştu.",
                    ErrorCodes = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDto<object>> DeleteTable(int id)
        {
            try
            {
                var repository = await _tableRepository.GetByIdAsync(id);
                if (repository == null)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Data = null,
                        Message = "Aradığınız masa bulunamadı",
                        ErrorCodes = ErrorCodes.NotFound
                    };
                }
                await _tableRepository.DeleteAsync(repository);
                return new ResponseDto<object>
                {
                    Success = true,
                    Data = null,
                    Message = "Masa başarılı bir şekilde silindi."
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto<object>
                {
                    Success = false,
                    Data = null,
                    Message = "Bir hata oluştu.",
                    ErrorCodes = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDto<List<ResultTableDto>>> GetAllTables()
        {
            try
            {
                var repository = await _tableRepository.GetAllAsync();
                if (repository.Count == 0)
                {
                    return new ResponseDto<List<ResultTableDto>> 
                    {
                        Success = false,
                        Data = null,
                        Message = "Masalar bulunamadı.",
                        ErrorCodes = ErrorCodes.NotFound
                    };
                }
                var result = _mapper.Map<List<ResultTableDto>>(repository);
                return new ResponseDto<List<ResultTableDto>>
                {
                    Success = true,
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto<List<ResultTableDto>>
                {
                    Success = false,
                    Data = null,
                    Message = "Bir sorun oluştu.",
                    ErrorCodes = ErrorCodes.Exception
                };

            }
        }

        public async Task<ResponseDto<DetailTableDto>> GetByIdTable(int id)
        {
            try
            {
                var repository = await _tableRepository.GetByIdAsync(id);
                if (repository == null)
                {
                    return new ResponseDto<DetailTableDto>
                    {
                        Success = false,
                        Data = null,
                        Message ="Masa bulunamadı.",
                        ErrorCodes = ErrorCodes.NotFound
                    };
                }
                var result = _mapper.Map<DetailTableDto>(repository);
                return new ResponseDto<DetailTableDto> 
                {
                    Success = true,
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto<DetailTableDto>
                {
                    Success = false,
                    Data = null,
                    Message = "Bir sorun oluştu.",
                    ErrorCodes = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDto<DetailTableDto>> GetByTableNumber(int tabloNumber)
        {
            try
            {
                var table = await _tableRepository1.GetByTableNumberAsync(tabloNumber);
                if (table == null)
                {
                    return new ResponseDto<DetailTableDto>
                    {
                        Success = false,
                        Data = null,
                        Message = "Masa bulunamadı.",
                        ErrorCodes = ErrorCodes.NotFound
                    };
                }
                var result = _mapper.Map<DetailTableDto>(table);
                return new ResponseDto<DetailTableDto> 
                {
                    Success = true, 
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto<DetailTableDto>
                {
                    Success = false,
                    Data = null,
                    Message ="Bir hata oluştu.",
                    ErrorCodes= ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDto<object>> UpdateTable(UpdateTableDto dto)
        {
            try
            {
                var validate = await _updateTableValidator.ValidateAsync(dto);
                if (!validate.IsValid)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Data = null,
                        Message = string.Join(" | ",validate.Errors.Select(x=>x.ErrorMessage)),
                        ErrorCodes = ErrorCodes.ValidationError
                    };
                }

                //var checkTable = await _tableRepository.GetByIdAsync(dto.TableNumber);
                var repository = await _tableRepository.GetByIdAsync(dto.Id);
                if (repository == null)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Data = null,
                        Message ="Masa bulunamadı.",
                        ErrorCodes = ErrorCodes.NotFound
                    };
                }
                var result = _mapper.Map(dto, repository);
                await _tableRepository.UpdateAsync(result);
                return new ResponseDto<object> 
                {
                    Success = true,
                    Data= result,
                    Message = "Masa başarılı bir şekilde güncellendi."
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto<object>
                {
                    Success = false,
                    Data = null,
                    Message = "Bir hata oluştu.",
                    ErrorCodes = ErrorCodes.Exception
                };
            }
        }
    }
}
