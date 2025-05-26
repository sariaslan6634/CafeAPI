using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CafeAPI.Application.Dtos.CategoryDto;
using CafeAPI.Application.Dtos.MenuItemDto;
using CafeAPI.Application.Dtos.OrderDto;
using CafeAPI.Application.Dtos.OrderItemDto;
using CafeAPI.Application.Dtos.TableDto;
using CafeAPI.Domain.Entities;

namespace CafeAPI.Application.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<Category, CreateCategoryDto>().ReverseMap();
            CreateMap<Category, ResultCategoryDto>().ReverseMap();
            CreateMap<Category, UpdateCategoryDto>().ReverseMap();
            CreateMap<Category, DetailCategoryDto>().ReverseMap();
            CreateMap<Category, ResultCategoriesWithMenuDto>().ReverseMap();

            
            CreateMap<MenuItem, CreateMenuItemDto>().ReverseMap();
            CreateMap<MenuItem, ResultMenuItemDto>().ReverseMap();
            CreateMap<MenuItem, UpdateMenuItemDto>().ReverseMap();
            CreateMap<MenuItem, DetailMenuItemDto>().ReverseMap();
            CreateMap<MenuItem, CategoriesMenuItemDto>().ReverseMap();

            CreateMap<Table, ResultTableDto>().ReverseMap();
            CreateMap<Table, DetailTableDto>().ReverseMap();
            CreateMap<Table, CreateTableDto>().ReverseMap();
            CreateMap<Table, UpdateTableDto>().ReverseMap();

            CreateMap<OrderItem,ResultOrderItemDto>().ReverseMap();
            CreateMap<OrderItem,DetailOrderItemDto>().ReverseMap();
            CreateMap<OrderItem,CreateOrderItemDto>().ReverseMap();
            CreateMap<OrderItem,UpdateOrderItemDto>().ReverseMap();

            CreateMap<Order, ResultOrderDto>().ReverseMap();
            CreateMap<Order, DetailOrderDto>().ReverseMap();
            CreateMap<Order, CreateOrderDto>().ReverseMap();
            CreateMap<Order, UpdateOrderDto>().ReverseMap();
        }
    }
}
