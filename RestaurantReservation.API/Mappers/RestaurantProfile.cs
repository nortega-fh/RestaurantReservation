using AutoMapper;
using RestaurantReservation.API.Contracts.Requests;
using RestaurantReservation.API.Contracts.Responses;
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