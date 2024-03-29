using RestaurantReservation.Domain.Models;

namespace RestaurantReservation.Domain.Services;

public interface IUserService
{
    Task<User?> GetAsync(string username, string password);
    Task<User> CreateAsync(User user);
    Task UpdateAsync(string id, User user);
    Task DeleteAsync(string id);
}