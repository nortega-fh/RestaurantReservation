using MongoDB.Driver;
using MongoDB.Driver.Linq;
using RestaurantReservation.Domain.Tables;
using Table = RestaurantReservation.Domain.Tables.Table;

namespace RestaurantReservation.Infrastructure.Repositories;

public class TableRepository : ITableRepository
{
    private readonly IMongoCollection<Table> _collection;

    public TableRepository(IRestaurantReservationDatabase database)
    {
        _collection = database.GetDatabase().GetCollection<Table>("Tables");
    }

    public async Task<IEnumerable<Table>> GetAllAsync(string restaurantId, int pageSize, int pageNumber)
    {
        return await _collection
            .AsQueryable()
            .Where(table => table.RestaurantId.Equals(restaurantId))
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<Table?> GetByIdAsync(string tableId)
    {
        return await _collection
            .AsQueryable()
            .Where(table => table.Id.Equals(tableId))
            .FirstOrDefaultAsync();
    }

    public async Task CreateAsync(Table table)
    {
        await _collection.InsertOneAsync(table);
    }

    public async Task UpdateAsync(string tableId, Table table)
    {
        var filter = Builders<Table>.Filter.Eq(t => t.Id, tableId);
        await _collection.FindOneAndReplaceAsync(filter, table);
    }

    public async Task DeleteAsync(string tableId)
    {
        var filter = Builders<Table>.Filter.Eq(t => t.Id, tableId);
        await _collection.DeleteOneAsync(filter);
    }
}