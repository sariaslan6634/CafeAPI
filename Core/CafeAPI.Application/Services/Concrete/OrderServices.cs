using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CafeAPI.Application.Dtos.OrderDto;
using CafeAPI.Application.Dtos.ResponseDto;
using CafeAPI.Application.Interfaces;
using CafeAPI.Application.Services.Abstract;
using CafeAPI.Domain.Entities;
using FluentValidation;

namespace CafeAPI.Application.Services.Concrete
{
    public class OrderServices : IOrderServices
    {
        private readonly IGenericRepository<Order> _orderRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateOrderDto> _createOrdervalidator;
        private readonly IValidator<UpdateOrderDto> _updateOrdervalidator;

        public OrderServices(IGenericRepository<Order> genericRepository, IMapper mapper, IValidator<CreateOrderDto> validator, IValidator<UpdateOrderDto> updateOrdervalidator)
        {
            _orderRepository = genericRepository;
            _mapper = mapper;
            _createOrdervalidator = validator;
            _updateOrdervalidator = updateOrdervalidator;
        }

        public async Task<ResponseDto<object>> AddOrder(CreateOrderDto dto)
        {
            try
            {
                var validate = await _createOrdervalidator.ValidateAsync(dto);
                if (!validate.IsValid)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Data = null,
                        Message = string.Join(" | ",validate.Errors.Select(x=>x.ErrorMessage)),
                        ErrorCode = ErrorCodes.ValidationError
                    };
                }
                var result = _mapper.Map<Order>(dto);
                await _orderRepository.AddAsync(result);
                return new ResponseDto<object> 
                {
                    Success = true,
                    Data = null,
                    Message = "Siparişiniz eklendi"                    
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto<object> 
                {
                    Success = false,
                    Data = null,
                    Message = "Bir sorun oluştu",
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDto<object>> DeleteOrder(int id)
        {
            try
            {
                var db = await _orderRepository.GetByIdAsync(id);
                if (db == null)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Data = null,
                        Message = "Sipariş bulunamadı",
                        ErrorCode = ErrorCodes.NotFound
                    };
                }
                await _orderRepository.DeleteAsync(db);
                return new ResponseDto<object>
                {
                    Success = true,
                    Data = null,
                    Message = "Siparişiniz silindi",
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto<object>
                {
                    Success = false,
                    Data = null,
                    Message ="Bir hata oluştu",
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDto<List<ResultOrderDto>>> GetAllOrder()
        {
            try
            {
                var orderdb = await _orderRepository.GetAllAsync();
                if (orderdb.Count == 0)
                {
                    return new ResponseDto<List<ResultOrderDto>>
                    {
                        Success = false,
                        Data = null,
                        Message = "Sipariş bulunamadı.",
                        ErrorCode = ErrorCodes.NotFound
                    };
                }
                var result = _mapper.Map<List<ResultOrderDto>>(orderdb);
                return new ResponseDto<List<ResultOrderDto>>
                {
                    Success = true,
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto<List<ResultOrderDto>>
                {
                    Success = false,
                    Data = null,
                    Message = "Bir sorun oluştu",
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDto<DetailOrderDto>> GetOrderById(int id)
        {
            try
            {
                var db = await _orderRepository.GetByIdAsync(id);
                if (db == null)
                {
                    return new ResponseDto<DetailOrderDto>
                    {
                        Success = false,
                        Data = null,
                        Message = "Sipariş bulunamadı.",
                        ErrorCode = ErrorCodes.NotFound
                    };
                }
                var result = _mapper.Map<DetailOrderDto>(db);
                return new ResponseDto<DetailOrderDto>
                {
                    Success = true,
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto<DetailOrderDto>
                {
                    Success = false,
                    Data = null,
                    Message = "Bir sorun oluştu",
                    ErrorCode = ErrorCodes.Exception

                };
            }
        }

        public async Task<ResponseDto<object>> UpdateOrder(UpdateOrderDto dto)
        {
            try
            {
                var validate = await _updateOrdervalidator.ValidateAsync(dto);
                if (!validate.IsValid)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Data = null,
                        Message = string.Join(" | ",validate.Errors.Select(x=>x.ErrorMessage)),
                        ErrorCode = ErrorCodes.ValidationError
                    };
                }
                var db =await _orderRepository.GetByIdAsync(dto.Id);
                if (db == null)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Data = null,
                        Message = "Sipariş bulunamadı.",
                        ErrorCode = ErrorCodes.NotFound
                    };
                }
                var result = _mapper.Map(dto, db);
                await _orderRepository.UpdateAsync(result);
                return new ResponseDto<object>
                {
                    Success = true,
                    Data = null,
                    Message = "Siparişiniz başarılı bir şekilde güncellendi.",
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto<object>
                {
                    Success = false,
                    Data = null,
                    Message = "Bir sorun oluştu.",
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }
    }
}
