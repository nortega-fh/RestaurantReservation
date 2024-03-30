using RestaurantReservation.Domain.Models;

namespace RestaurantReservation.Domain.Repositories;

public interface IRestaurantRepository
{
    Task<IEnumerable<Restaurant>> GetAllAsync(int pageSize, int pageNumber);
    Task<Restaurant?> GetByIdAsync(string id);
    Task<Restaurant?> CreateAsync(Restaurant restaurant);
    Task UpdateAsync(string id, Restaurant restaurant);
    Task DeleteAsync(string id);
}