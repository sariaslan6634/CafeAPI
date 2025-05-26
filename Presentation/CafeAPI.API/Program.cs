using CafeAPI.Application.Dtos.CategoryDto;
using CafeAPI.Application.Interfaces;
using CafeAPI.Application.Mapping;
using CafeAPI.Application.Services.Abstract;
using CafeAPI.Application.Services.Concrete;
using CafeAPI.Persistance.Context;
using CafeAPI.Persistance.Repository;
using Microsoft.EntityFrameworkCore;
using CafeAPI.Application.Validations;
using Scalar.AspNetCore;
using FluentValidation;
using CafeAPI.Application.Dtos.MenuItemDto;
using CafeAPI.Application.Dtos.TableDto;
using FluentValidation.AspNetCore;
using CafeAPI.Application.Dtos.OrderDto;
using CafeAPI.Application.Dtos.OrderItemDto;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
{
    var conf = builder.Configuration;
    var database = conf.GetConnectionString("DefaultConnection");
    options.UseSqlServer(database);
});

builder.Services.AddControllers();

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<ITableRepository, TableRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IMenuItemRepository, MenuItemRepository>();
builder.Services.AddScoped<IMenuItemServices, MenuItemServices>();
builder.Services.AddScoped<ICategoryServices, CategoryServices>();
builder.Services.AddScoped<ITableServices, TableServices>();
builder.Services.AddScoped<IOrderServices, OrderServices>();
builder.Services.AddScoped<IOrderItemServices, OrderItemServices>();

builder.Services.AddAutoMapper(typeof(GeneralMapping));

//Validator
builder.Services.AddValidatorsFromAssemblyContaining<CreateCategoryDto>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateCategoryDto>();

builder.Services.AddValidatorsFromAssemblyContaining<CreateMenuItemDto>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateMenuItemDto>();

builder.Services.AddValidatorsFromAssemblyContaining<CreateTableDto>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateTableDto>();

builder.Services.AddValidatorsFromAssemblyContaining<CreateOrderDto>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateOrderDto>();

builder.Services.AddValidatorsFromAssemblyContaining<CreateOrderItemDto>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateOrderItemDto>();


// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

builder.Services.AddEndpointsApiExplorer();
app.MapScalarApiReference(opt => {
    opt.Title = "Kafe v1";
    opt.Theme = ScalarTheme.BluePlanet;
    opt.DefaultHttpClient = new(ScalarTarget.Http, ScalarClient.Http11);
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
