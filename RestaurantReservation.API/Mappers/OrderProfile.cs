using AutoMapper;
using RestaurantReservation.API.Contracts.Requests;
using RestaurantReservation.API.Contracts.Responses;
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