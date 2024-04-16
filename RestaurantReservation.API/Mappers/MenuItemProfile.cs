using AutoMapper;
using RestaurantReservation.API.Contracts.Requests.MenuItems;
using RestaurantReservation.API.Contracts.Responses.MenuItems;
using RestaurantReservation.Domain.MenuItems;

namespace RestaurantReservation.API.Mappers;

public class MenuItemProfile : Profile
{
    public MenuItemProfile()
    {
        CreateMap<MenuItem, MenuItemResponse>();
        CreateMap<MenuItemCreate, MenuItem>();
    }
}