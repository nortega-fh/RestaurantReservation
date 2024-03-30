using AutoMapper;
using RestaurantReservation.API.Contracts.Responses;
using RestaurantReservation.Domain.Models;

namespace RestaurantReservation.API.Mappers;

public class RestaurantProfile : Profile
{
    public RestaurantProfile()
    {
        CreateMap<Restaurant, RestaurantResponse>();
    }
}