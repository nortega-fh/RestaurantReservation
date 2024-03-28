using RestaurantReservation.API.Dtos.Responses;
using RestaurantReservation.API.Entities;

namespace RestaurantReservation.API.Services;

public interface ITokenGenerator
{
    TokenResponse GenerateTokenAsync(User user);
}