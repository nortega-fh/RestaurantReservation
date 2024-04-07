using RestaurantReservation.Domain.Restaurants;

namespace RestaurantReservation.API.Contracts.Requests;

public record RestaurantCreate(
    string Name,
    string Address,
    string PhoneNumber,
    RestaurantSchedule Schedule);