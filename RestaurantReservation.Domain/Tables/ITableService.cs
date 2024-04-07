namespace RestaurantReservation.Domain.Tables;

public interface ITableService
{
    Task<IEnumerable<Table>> GetAllAsync(string restaurantId, int pageSize, int pageNumber);
    Task<Table?> GetByIdAsync(string tableId);
    Task CreateAsync(Table table);
    Task UpdateAsync(Table table);
    Task DeleteAsync(string tableId);
    Task<bool> ExistsWithIdAsync(string tableId);
}