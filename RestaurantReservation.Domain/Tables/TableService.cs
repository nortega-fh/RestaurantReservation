namespace RestaurantReservation.Domain.Tables;

public class TableService : ITableService
{
    private readonly ITableRepository _repository;

    public TableService(ITableRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<IEnumerable<Table>> GetAllAsync(string restaurantId, int pageSize, int pageNumber)
    {
        return await _repository.GetAllAsync(restaurantId, pageSize, pageNumber);
    }

    public async Task<Table?> GetByIdAsync(string tableId)
    {
        return await _repository.GetByIdAsync(tableId);
    }

    public async Task CreateAsync(Table table)
    {
        await _repository.CreateAsync(table);
    }

    public async Task UpdateAsync(string tableId, Table table)
    {
        await _repository.UpdateAsync(tableId, table);
    }

    public async Task DeleteAsync(string tableId)
    {
        await _repository.DeleteAsync(tableId);
    }

    public async Task<bool> ExistsWithIdAsync(string tableId)
    {
        return await GetByIdAsync(tableId) is not null;
    }
}