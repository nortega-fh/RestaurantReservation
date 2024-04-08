using AutoMapper;
using RestaurantReservation.API.Contracts.Requests;
using RestaurantReservation.API.Contracts.Responses;
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