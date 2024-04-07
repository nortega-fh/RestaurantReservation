namespace RestaurantReservation.Domain.Users;

public interface IUserService
{
    Task<User?> GetByCredentialsAsync(string username, string password);
    Task<User?> GetByUsernameAsync(string username);
    Task<User> CreateAsync(User user);
    Task UpdateAsync(string id, User user);
    Task DeleteAsync(string id);
    Task<bool> UserExistsAsync(string username);
}