using RestaurantReservation.API.Contracts.Responses;
using RestaurantReservation.Domain.Users;

namespace RestaurantReservation.API.AuthHandlers;

public interface ITokenGenerator
{
    TokenResponse GenerateTokenAsync(User user);
}