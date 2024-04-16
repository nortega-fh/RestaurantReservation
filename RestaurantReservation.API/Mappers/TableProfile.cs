using AutoMapper;
using RestaurantReservation.API.Contracts.Requests.Tables;
using RestaurantReservation.Domain.Tables;

namespace RestaurantReservation.API.Mappers;

public class TableProfile : Profile
{
    public TableProfile()
    {
        CreateMap<TableCreate, Table>();
    }
}