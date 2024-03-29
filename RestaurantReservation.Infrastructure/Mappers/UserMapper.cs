using AutoMapper;
using RestaurantReservation.Infrastructure.Entities;

namespace RestaurantReservation.Infrastructure.Mappers;

public class UserMapper : Profile
{
    public UserMapper()
    {
        CreateMap<User, Domain.Models.User>();
        CreateMap<Domain.Models.User, User>();
    }
}