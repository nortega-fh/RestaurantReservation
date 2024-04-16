using RestaurantReservation.API.Contracts.Responses.API;
using RestaurantReservation.Domain.Users;

namespace RestaurantReservation.API.AuthHandlers;

public interface ITokenGenerator
{
    TokenResponse GenerateTokenAsync(User user);
}