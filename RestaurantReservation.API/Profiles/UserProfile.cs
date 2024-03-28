using AutoMapper;
using RestaurantReservation.API.Dtos.Requests;
using RestaurantReservation.API.Entities;

namespace RestaurantReservation.API.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserCreate, User>();
    }
}