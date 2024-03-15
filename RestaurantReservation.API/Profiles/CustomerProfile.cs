using AutoMapper;
using RestaurantReservation.API.Dtos.Requests;
using RestaurantReservation.API.Entities;

namespace RestaurantReservation.API.Profiles;

public class CustomerProfile : Profile
{
    public CustomerProfile()
    {
        CreateMap<CustomerCreateDto, Customer>();
    }
}