namespace RestaurantReservation.Domain.Tables;

public interface ITableRepository
{
    Task<IEnumerable<Table>> GetAllAsync(string restaurantId, int pageSize, int pageNumber);
    Task<Table?> GetByIdAsync(string tableId);
    Task CreateAsync(Table table);
    Task UpdateAsync(string tableId, Table table);
    Task DeleteAsync(string tableId);
}