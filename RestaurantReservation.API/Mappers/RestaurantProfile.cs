using AutoMapper;
using RestaurantReservation.API.Contracts.Requests.Restaurants;
using RestaurantReservation.API.Contracts.Responses.Restaurants;
using RestaurantReservation.Domain.Restaurants;

namespace RestaurantReservation.API.Mappers;

public class RestaurantProfile : Profile
{
    public RestaurantProfile()
    {
        CreateMap<RestaurantCreate, Restaurant>();
        CreateMap<Restaurant, RestaurantResponse>();
    }
}