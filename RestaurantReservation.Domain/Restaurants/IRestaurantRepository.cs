namespace RestaurantReservation.Domain.Restaurants;

public interface IRestaurantRepository
{
    Task<IEnumerable<Restaurant>> GetAllAsync(int pageSize, int pageNumber);
    Task<Restaurant?> GetByIdAsync(string id);
    Task CreateAsync(Restaurant restaurant);
    Task UpdateAsync(Restaurant restaurant);
    Task DeleteAsync(string id);
}