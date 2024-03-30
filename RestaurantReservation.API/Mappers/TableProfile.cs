using AutoMapper;
using RestaurantReservation.API.Contracts.Requests;
using RestaurantReservation.Domain.Models;

namespace RestaurantReservation.API.Mappers;

public class TableProfile : Profile
{
    public TableProfile()
    {
        CreateMap<TableCreate, Table>();
    }
}