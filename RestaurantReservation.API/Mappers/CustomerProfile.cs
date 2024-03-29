using AutoMapper;
using RestaurantReservation.API.Contracts.Requests;
using RestaurantReservation.Domain.Models;

namespace RestaurantReservation.API.Mappers;

public class CustomerProfile : Profile
{
    public CustomerProfile()
    {
        CreateMap<CustomerCreate, Customer>();
    }
}