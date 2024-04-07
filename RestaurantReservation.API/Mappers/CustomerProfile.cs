using AutoMapper;
using RestaurantReservation.API.Contracts.Requests;
using RestaurantReservation.API.Contracts.Responses;
using RestaurantReservation.Domain.Customers;
using RestaurantReservation.Domain.Users;

namespace RestaurantReservation.API.Mappers;

public class CustomerProfile : Profile
{
    public CustomerProfile()
    {
        CreateMap<CustomerCreate, Customer>();
        CreateMap<CustomerUpdate, Customer>();
        CreateMap<Customer, CustomerResponse>();
        CreateMap<User, Customer>();
        CreateMap<Customer, User>();
    }
}