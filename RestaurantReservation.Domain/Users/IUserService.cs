namespace RestaurantReservation.Domain.Users;

public interface IUserService
{
    Task<User?> GetByCredentialsAsync(string username, string password);
    Task<User> CreateAsync(User user);
    Task<bool> UserExistsAsync(string username);
}