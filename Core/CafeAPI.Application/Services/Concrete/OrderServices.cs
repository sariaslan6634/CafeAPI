using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CafeAPI.Application.Dtos.OrderDto;
using CafeAPI.Application.Dtos.OrderItemDto;
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
        private readonly IGenericRepository<OrderItem> _orderItemRepository;
        private readonly IGenericRepository<MenuItem> _menuItemRepository;
        private readonly IOrderRepository _orderRepository2;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateOrderDto> _createOrdervalidator;
        private readonly IValidator<UpdateOrderDto> _updateOrdervalidator;

        public OrderServices(IGenericRepository<Order> genericRepository, IMapper mapper, IValidator<CreateOrderDto> validator, IValidator<UpdateOrderDto> updateOrdervalidator, IGenericRepository<OrderItem> orderItemRepository, IOrderRepository orderRepository2, IGenericRepository<MenuItem> menuItemRepository)
        {
            _orderRepository = genericRepository;
            _mapper = mapper;
            _createOrdervalidator = validator;
            _updateOrdervalidator = updateOrdervalidator;
            _orderItemRepository = orderItemRepository;
            _orderRepository2 = orderRepository2;
            _menuItemRepository = menuItemRepository;
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
                result.Status = OrderStatus.Hazirlaniyor;
                result.CreatedAt = DateTime.Now;
                decimal totalPrice = 0; 
                foreach (var item in result.OrderItems)
                {
                    item.MenuItem = await _menuItemRepository.GetByIdAsync(item.MenuItemId);
                    item.Price = item.MenuItem.Price * item.Quantity;
                    totalPrice += item.Price;
                }
                result.TotalPrice = totalPrice;
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

        //public async Task<ResponseDto<object>> AddOrderItemByOrderId(AddOrderItemByOrderDto dto)
        //{
        //    try
        //    {
        //        var db = await _orderRepository.GetByIdAsync(dto.OrderId);
        //        var orderItems = await _orderItemRepository.GetAllAsync();
        //        if (db == null)
        //        {
        //            return new ResponseDto<object>
        //            {
        //                Success = false,
        //                Data = null,
        //                Message = "Sipariş bulunamadı.",
        //                ErrorCode = ErrorCodes.NotFound
        //            };
        //        }
        //        var result = _mapper.Map<OrderItem>(dto.OrderItem);
        //        db.OrderItems.Add(result);
        //        await _orderRepository.UpdateAsync(db);
        //        return new ResponseDto<object>
        //        {
        //            Success = true,
        //            Data = null,
        //            Message = "Siparişiniz başarılı bir şekilde güncellendi.",
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ResponseDto<object>
        //        {
        //            Success = false,
        //            Data = null,
        //            Message = "Bir sorun oluştu.",
        //            ErrorCode = ErrorCodes.Exception
        //        };
        //    }
        //}

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
                var orderItemdb = await _orderItemRepository.GetAllAsync();
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
                var db = await _orderRepository2.GetOrderByIdWithDetailAsync(id);
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

        public async Task<ResponseDto<List<ResultOrderDto>>> GettAllOrdersWithDetail()
        {
            try
            {
                var orderdb = await _orderRepository2.GetAllOrderWithDetailAsync();
                var orderItemdb = await _orderItemRepository.GetAllAsync();
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
                result.UpdateAt = DateTime.Now;
                decimal totalprice = 0;
                foreach (var item in result.OrderItems)
                {
                    item.MenuItem = await _menuItemRepository.GetByIdAsync(item.MenuItemId);
                    item.Price = item.MenuItem.Price * item.Quantity;
                    totalprice += item.Price;
                }
                result.TotalPrice = totalprice;
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

        public async Task<ResponseDto<object>> UpdateOrderStatusHazir(int orderId)
        {
            try
            {                
                var db = await _orderRepository.GetByIdAsync(orderId);
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
                db.Status = OrderStatus.Hazir; 
                await _orderRepository.UpdateAsync(db);
                return new ResponseDto<object>
                {
                    Success = true,
                    Data = null,
                    Message = "Sipariş durumu hazır olarak güncellendi.",
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

        public async Task<ResponseDto<object>> UpdateOrderStatusIptalEdildi(int orderId)
        {
            try
            {
                var db = await _orderRepository.GetByIdAsync(orderId);
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
                db.Status = OrderStatus.IptalEdildi;
                await _orderRepository.UpdateAsync(db);
                return new ResponseDto<object>
                {
                    Success = true,
                    Data = null,
                    Message = "Sipariş durumu iptal edildi olarak güncellendi.",
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

        public async Task<ResponseDto<object>> UpdateOrderStatusTeslimEdildi(int orderId)
        {
            try
            {
                var db = await _orderRepository.GetByIdAsync(orderId);
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
                db.Status = OrderStatus.TeslimEdildi;
                await _orderRepository.UpdateAsync(db);
                return new ResponseDto<object>
                {
                    Success = true,
                    Data = null,
                    Message = "Sipariş durumu teslim edildi olarak güncellendi.",
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
