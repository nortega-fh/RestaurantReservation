using RestaurantReservation.API.Entities;

namespace RestaurantReservation.API.Repositories;

public interface IUserRepository
{
    Task<User?> GetByUsernameAsync(string username);
    Task UpdateAsync(string id, User user);
    Task DeleteAsync(string id);
    Task<User> CreateUserAsync(User user);
}