using AutoMapper;
using RestaurantReservation.API.Contracts.Requests.Orders;
using RestaurantReservation.API.Contracts.Responses.Orders;
using RestaurantReservation.Domain.Orders;

namespace RestaurantReservation.API.Mappers;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<Order, OrderReservationResponse>();
        CreateMap<OrderReservationCreate, Order>();
    }
}