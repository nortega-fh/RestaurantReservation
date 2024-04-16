using RestaurantReservation.Domain.Restaurants;

namespace RestaurantReservation.API.Contracts.Requests.Restaurants;

public record RestaurantCreate(
    string Name,
    string Address,
    string PhoneNumber,
    RestaurantSchedule Schedule);