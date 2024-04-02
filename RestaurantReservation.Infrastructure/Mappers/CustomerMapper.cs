using AutoMapper;
using RestaurantReservation.Infrastructure.Entities;
using User = RestaurantReservation.Domain.Models.User;

namespace RestaurantReservation.Infrastructure.Mappers;

public class CustomerMapper : Profile
{
    public CustomerMapper()
    {
        CreateMap<Customer, Domain.Models.Customer>();
        CreateMap<Domain.Models.Customer, Customer>();
        CreateMap<User, Customer>();
    }
}