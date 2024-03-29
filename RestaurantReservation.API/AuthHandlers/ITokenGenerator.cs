using RestaurantReservation.API.Contracts.Responses;
using RestaurantReservation.Domain.Models;

namespace RestaurantReservation.API.AuthHandlers;

public interface ITokenGenerator
{
    TokenResponse GenerateTokenAsync(User user);
}