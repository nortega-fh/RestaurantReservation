using AutoMapper;
using RestaurantReservation.API.Contracts.Requests.Reservations;
using RestaurantReservation.API.Contracts.Responses.Reservations;
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