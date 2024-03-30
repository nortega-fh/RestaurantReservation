using RestaurantReservation.Domain.Models;

namespace RestaurantReservation.Domain.Services;

public interface IRestaurantService
{
    Task<IEnumerable<Restaurant>> GetAllAsync(int pageSize, int pageNumber);
    Task<Restaurant?> GetByIdAsync(string id);
    Task<Restaurant?> CreateAsync(Restaurant restaurant);
    Task UpdateAsync(string id, Restaurant restaurant);
    Task DeleteAsync(string id);
    Task<bool> RestaurantExistsWithIdAsync(string id);
}