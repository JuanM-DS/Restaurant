using AutoMapper;
using Restaurant.Core.Application.DTOs.Entities;
using Restaurant.Core.Domain.Entities;

namespace Restaurant.Core.Application.Mappings
{
    public class GeneralProfiles : Profile
    {
        public GeneralProfiles()
        {
            CreateMap<Dish, DishDto>()
                .ReverseMap()
                .ForMember(des => des.Ordes, opt => opt.Ignore())
                .ForMember(des => des.Category, opt => opt.Ignore());

            CreateMap<Ingredient, IngredientDto>()
                .ReverseMap()
                .ForMember(des => des.Dishes, opt => opt.Ignore());

            CreateMap<DishCategory, DishCategoryDto>()
                .ReverseMap()
                .ForMember(des => des.Dishes, opt => opt.Ignore());

            CreateMap<Order, OrderDto>()
                .ReverseMap()
                .ForMember(des => des.Table, opt => opt.Ignore())
                .ForMember(des => des.SelectedDishes, opt => opt.Ignore())
                .ForMember(des => des.Status, opt => opt.Ignore());

            CreateMap<OrderStatus, OrderStatusDto>()
                .ReverseMap()
                .ForMember(des => des.Orders, opt => opt.Ignore());

            CreateMap<Table, TableDto>()
                .ReverseMap()
                .ForMember(des => des.Orders, opt => opt.Ignore())
                .ForMember(des => des.Status, opt => opt.Ignore());

            CreateMap<TableStatus, TableStatusDto>()
                .ReverseMap()
                .ForMember(des => des.Tables, opt => opt.Ignore());

            CreateMap<ApplicationUserDto, SaveApplicationUserDto>()
                .ForMember(des => des.Password, opt => opt.Ignore())
                .ForMember(des => des.ConfirmPassword, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(des => des.Roles, opt => opt.Ignore());
        }
    }
}
