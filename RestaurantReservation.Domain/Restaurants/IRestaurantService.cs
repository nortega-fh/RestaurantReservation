namespace RestaurantReservation.Domain.Restaurants;

public interface IRestaurantService
{
    Task<IEnumerable<Restaurant>> GetAllAsync(int pageSize, int pageNumber);
    Task<Restaurant?> GetByIdAsync(string id);
    Task CreateAsync(Restaurant restaurant);
    Task UpdateAsync(Restaurant restaurant);
    Task DeleteAsync(string id);
    Task<bool> RestaurantExistsWithIdAsync(string id);
}