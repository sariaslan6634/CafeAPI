using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CafeAPI.Application.Dtos.OrderItemDto;
using CafeAPI.Application.Dtos.ResponseDto;
using CafeAPI.Application.Interfaces;
using CafeAPI.Application.Services.Abstract;
using CafeAPI.Domain.Entities;
using FluentValidation;

namespace CafeAPI.Application.Services.Concrete
{
    public class OrderItemServices : IOrderItemServices
    {
        private readonly IGenericRepository<OrderItem> _orderItemRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateOrderItemDto> _createOrderItemValidator;
        private readonly IValidator<UpdateOrderItemDto> _updateOrderItemValidator;

        public OrderItemServices(IGenericRepository<OrderItem> genericRepository, IMapper mapper, IValidator<CreateOrderItemDto> createOrderItemValidator, IValidator<UpdateOrderItemDto> updateOrderItemValidator)
        {
            _orderItemRepository = genericRepository;
            _mapper = mapper;
            _createOrderItemValidator = createOrderItemValidator;
            _updateOrderItemValidator = updateOrderItemValidator;
        }

        public async Task<ResponseDto<object>> AddOrderItem(CreateOrderItemDto dto)
        {
            try
            {
                var validate = await _createOrderItemValidator.ValidateAsync(dto);
                if (!validate.IsValid)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Data = null,
                        Message = string.Join(" | ", validate.Errors.Select(x => x.ErrorMessage)),
                        ErrorCode = ErrorCodes.ValidationError
                    };
                }
                var result = _mapper.Map<OrderItem>(dto);
                await _orderItemRepository.AddAsync(result);
                return new ResponseDto<object>
                {
                    Success = true,
                    Data = null,
                    Message ="Sipariş iteminiz oluşturuldu."
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto<object>()
                {
                    Success = false,
                    Data = null,
                    Message = "Bir sorun oluştu",
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDto<object>> DeleteOrderItem(int dto)
        {
            try
            {
                var delete = await _orderItemRepository.GetByIdAsync(dto);
                if (delete == null)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Data = null,
                        Message = "Sipariş itemi bulunamadı.",
                        ErrorCode = ErrorCodes.NotFound
                    };
                }
                await _orderItemRepository.DeleteAsync(delete);
                return new ResponseDto<object>
                {
                    Success = true,
                    Data = null,
                    Message = "Sipariş iteminiz başarılı bir şekilde silindi.",
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto<object>()
                {
                    Success = false,
                    Data = null,
                    Message = "Bir sorun oluştu",
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDto<List<ResultOrderItemDto>>> GetAllOrderItems()
        {
            try
            {
                var db = await _orderItemRepository.GetAllAsync();
                if (db.Count == 0)
                    return new ResponseDto<List<ResultOrderItemDto>>
                    {
                        Success = false,
                        Data = null,
                        Message = "Sipariş bulunamadı.",
                        ErrorCode = ErrorCodes.NotFound
                    };               
                var result = _mapper.Map<List<ResultOrderItemDto>>(db);
                return new ResponseDto<List<ResultOrderItemDto>>
                {
                    Success = true,
                    Data = result
                };
                

            }
            catch (Exception ex)
            {
                return new ResponseDto<List<ResultOrderItemDto>>
                {
                    Success = false,
                    Data = null,
                    Message = "bir sorun oluştu",
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDto<DetailOrderItemDto>> GetOrderItemById(int id)
        {
            try
            {
                var db = _orderItemRepository.GetByIdAsync(id);
                if (db == null)
                    return new ResponseDto<DetailOrderItemDto> { 
                        Success = false,
                        Data = null,
                        Message ="Sipariş itemi bulunamadı.",
                        ErrorCode = ErrorCodes.NotFound
                    };
                var result = _mapper.Map<DetailOrderItemDto>(db);
                return new ResponseDto<DetailOrderItemDto>
                { 
                    Success = true,
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto<DetailOrderItemDto>
                {
                    Success = false,
                    Data = null,
                    Message = "Bir sorun oluştu",
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDto<object>> UpdetOrderItem(UpdateOrderItemDto dto)
        {
            try
            {
                var validate = await _updateOrderItemValidator.ValidateAsync(dto);
                if (!validate.IsValid)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Data = null,
                        Message = string.Join(" | ",validate.Errors.Select(x=>x.ErrorMessage)),
                        ErrorCode= ErrorCodes.ValidationError
                    };
                }
                var checkOrderItem =await _orderItemRepository.GetByIdAsync(dto.Id);
                if (checkOrderItem == null)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Data = false,
                        Message = "Sipariş itemi bulunamadı.",
                        ErrorCode = ErrorCodes.NotFound
                    };
                }
                var result = _mapper.Map(dto,checkOrderItem);
                await _orderItemRepository.UpdateAsync(result);
                return new ResponseDto<object>
                {
                    Success = true,
                    Data = null,
                    Message = "Sipariş iteminiz başarılı bir şekilde güncellendi."
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
    }
}
