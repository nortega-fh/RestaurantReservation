using AutoMapper;
using RestaurantReservation.Infrastructure.Entities;

namespace RestaurantReservation.Infrastructure.Mappers;

public class TableMapper : Profile
{
    public TableMapper()
    {
        CreateMap<Table, Domain.Models.Table>();
        CreateMap<Domain.Models.Table, Table>();
    }
}