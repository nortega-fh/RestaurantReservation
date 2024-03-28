using RestaurantReservation.API.Entities;

namespace RestaurantReservation.API.Services;

public interface IUserService
{
    Task<User?> GetAsync(string username, string password);
    Task<User> CreateAsync(User user);
    Task UpdateAsync(string id, User user);
    Task DeleteAsync(string id);
}