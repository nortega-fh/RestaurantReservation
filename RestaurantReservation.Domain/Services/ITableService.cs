using RestaurantReservation.Domain.Models;

namespace RestaurantReservation.Domain.Services;

public interface ITableService
{
    Task<IEnumerable<Table>> GetAllAsync(string restaurantId, int pageSize, int pageNumber);
    Task<Table?> GetByIdAsync(string tableId);
    Task CreateAsync(string restaurantId, Table table);
    Task UpdateAsync(string tableId, Table table);
    Task DeleteAsync(string tableId);
    Task<bool> ExistsWithIdAsync(string tableId);
}