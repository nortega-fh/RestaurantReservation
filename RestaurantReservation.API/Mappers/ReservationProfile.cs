using AutoMapper;
using RestaurantReservation.API.Contracts.Requests;
using RestaurantReservation.API.Contracts.Responses;
using RestaurantReservation.Domain.Reservations;

namespace RestaurantReservation.API.Mappers;

public class ReservationProfile : Profile
{
    public ReservationProfile()
    {
        CreateMap<ReservationCreate, Reservation>();
        CreateMap<Reservation, ReservationResponse>();
    }
}