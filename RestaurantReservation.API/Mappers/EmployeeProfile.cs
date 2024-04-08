using AutoMapper;
using RestaurantReservation.API.Contracts.Requests;
using RestaurantReservation.API.Contracts.Responses;
using RestaurantReservation.Domain.Employees;

namespace RestaurantReservation.API.Mappers;

public class EmployeeProfile : Profile
{
    public EmployeeProfile()
    {
        CreateMap<Employee, EmployeeResponse>();
        CreateMap<EmployeeCreate, Employee>();
        CreateMap<EmployeeUpdate, Employee>();
    }
}