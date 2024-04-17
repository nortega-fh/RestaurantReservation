namespace RestaurantReservation.Domain.Users;

public interface IUserRepository
{
    Task<User?> GetByUsernameAsync(string username);
    Task<User> CreateUserAsync(User user);
}