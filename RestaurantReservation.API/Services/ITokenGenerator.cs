using RestaurantReservation.API.Dtos.Responses;

namespace RestaurantReservation.API.Services;

public interface ITokenGenerator
{
   TokenResponse GenerateToken(string username, string password);
}