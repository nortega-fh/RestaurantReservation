using AutoMapper;
using RestaurantReservation.Domain.Models;

namespace RestaurantReservation.Infrastructure.Mappers;

public class RestaurantMapper : Profile
{
    public RestaurantMapper()
    {
        CreateMap<Restaurant, Entities.Restaurant>();
        CreateMap<Entities.Restaurant, Restaurant>();
    }
}