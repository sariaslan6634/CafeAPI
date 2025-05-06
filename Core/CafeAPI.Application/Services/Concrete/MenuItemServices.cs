using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CafeAPI.Application.Dtos.MenuItemDto;
using CafeAPI.Application.Dtos.ResponseDto;
using CafeAPI.Application.Interfaces;
using CafeAPI.Application.Services.Abstract;
using CafeAPI.Domain.Entities;
using FluentValidation;

namespace CafeAPI.Application.Services.Concrete
{
    public class MenuItemServices : IMenuItemServices
    {
        private readonly IValidator<CreateMenuItemDto> _createMenuItemValidator;
        private readonly IValidator<UpdateMenuItemDto> _updateMenuItemValidator;
        private readonly IGenericRepository<MenuItem> _menuItemsRepository;
        private readonly IGenericRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;

        public MenuItemServices(IGenericRepository<MenuItem> menuItemsRepository, IMapper mapper, IValidator<CreateMenuItemDto> createMenuItemValidator, IValidator<UpdateMenuItemDto> updateMenuItemValidator, IGenericRepository<Category> categoryRepository)
        {
            _menuItemsRepository = menuItemsRepository;
            _mapper = mapper;
            _createMenuItemValidator = createMenuItemValidator;
            _updateMenuItemValidator = updateMenuItemValidator;
            _categoryRepository = categoryRepository;
        }

        public async Task<ResponseDto<object>> AddManuItem(CreateMenuItemDto dto)
        {
            try
            {
                var validate = await _createMenuItemValidator.ValidateAsync(dto);
                if (!validate.IsValid)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Data = null,
                        Message = string.Join(" | ", validate.Errors.Select(x => x.ErrorMessage)),
                        ErrorCodes = ErrorCodes.ValidationError
                    };
                }
                var checkCategory = await _categoryRepository.GetByIdAsync(dto.CategoryId);
                if (checkCategory == null)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Data = dto,
                        Message ="Eklemek istediğiniz categori bulunamadı.",
                        ErrorCodes = ErrorCodes.NotFound
                    };
                }
                var menuItem = _mapper.Map<MenuItem>(dto);
                await _menuItemsRepository.AddAsync(menuItem);
                return new ResponseDto<object> { Success = true, Data = null,Message = "Menu item başarılı bir şekilde eklendi." };

            }
            catch (Exception ex)
            {
                return new ResponseDto<object>{Success = false,Data = null,Message = "Bir hata oluştu",ErrorCodes = ErrorCodes.Exception};
            }
        }

        public async Task<ResponseDto<object>> DeleteManuItem(int id)
        {
            try
            {
                var menuItem = await _menuItemsRepository.GetByIdAsync(id);
                if (menuItem == null)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Data = null,
                        Message = "Menu item bulunamadı.",
                        ErrorCodes = ErrorCodes.NotFound
                    };
                }
                await _menuItemsRepository.DeleteAsync(menuItem);
                return new ResponseDto<object> { Success = true, Data = null, Message = "Menu item başarılı bir şekilde silindi.." };
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

        public async Task<ResponseDto<List<ResultMenuItemDto>>> GetAllMenuItems()
        {
            try
            {
                var menuItems = await _menuItemsRepository.GetAllAsync();
                var category = await _categoryRepository.GetAllAsync();
                if (menuItems.Count == 0)
                {
                    return new ResponseDto<List<ResultMenuItemDto>>
                    {
                        Success = false,
                        Data = null,
                        Message = "Menu items bulunamadı.",
                        ErrorCodes = ErrorCodes.NotFound
                    };
                }
                var result = _mapper.Map<List<ResultMenuItemDto>>(menuItems);
                return new ResponseDto<List<ResultMenuItemDto>>
                {
                    Success = true,
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto<List<ResultMenuItemDto>>{
                    Success = false,
                    Data = null,
                    Message ="Bir hata Oluştu",
                    ErrorCodes = ErrorCodes.Exception
                };
            }           
        }

        public async Task<ResponseDto<DetailMenuItemDto>> GetByIdMenuItem(int id)
        {
            try
            {
                var menuItem = await _menuItemsRepository.GetByIdAsync(id);
                var category = await _categoryRepository.GetByIdAsync(menuItem.Id);
                if (menuItem == null)
                    return new ResponseDto<DetailMenuItemDto> { Success = false, Data = null, Message = "Menu item bulunamadı.", ErrorCodes = ErrorCodes.NotFound };
                var result = _mapper.Map<DetailMenuItemDto>(menuItem);
                return new ResponseDto<DetailMenuItemDto> { Success = true, Data = result };
            }
            catch (Exception ex)
            {
                return new ResponseDto<DetailMenuItemDto> { Success = false, Data = null, Message = "Bir hata oluştu.", ErrorCodes = ErrorCodes.Exception };
            }
            
        }

        public async Task<ResponseDto<object>> UpdateManuItem(UpdateMenuItemDto dto)
        {
            try
            {
                var validate = await _updateMenuItemValidator.ValidateAsync(dto);
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
                var menuitem = await _menuItemsRepository.GetByIdAsync(dto.Id);
                if (menuitem == null)
                {
                    return new ResponseDto<object>
                    {
                        Success =false,
                        Data = null,
                        Message = "Menu item bulunamadı.",
                        ErrorCodes = ErrorCodes.NotFound
                    };
                }
                var checkCategory = await _categoryRepository.GetByIdAsync(dto.CategoryId);
                if (checkCategory == null)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Data = dto,
                        Message = "Eklemek istediğiniz categori bulunamadı.",
                        ErrorCodes = ErrorCodes.NotFound
                    };
                }
                var newMenuItem = _mapper.Map(dto, menuitem);
                await _menuItemsRepository.UpdateAsync(newMenuItem);
                return new ResponseDto<object>
                {
                    Success = true,
                    Data = null,
                    Message ="Menu item başarılı bir şekilde güncellendi."
                };

            }
            catch (Exception ex)
            {
                return new ResponseDto<object> { Success = false, Data = null, Message = "Bir hata oluştu.", ErrorCodes = ErrorCodes.Exception };
            }
        }
    }
}
