using AutoMapper;
using RestaurantReservation.Infrastructure.Entities;

namespace RestaurantReservation.Infrastructure.Mappers;

public class CustomerMapper : Profile
{
    public CustomerMapper()
    {
        CreateMap<Customer, Domain.Models.Customer>();
        CreateMap<Domain.Models.Customer, Customer>();
    }
}