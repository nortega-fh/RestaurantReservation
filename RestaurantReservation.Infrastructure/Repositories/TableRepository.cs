using AutoMapper;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using RestaurantReservation.Domain.Repositories;
using RestaurantReservation.Infrastructure.Entities;

namespace RestaurantReservation.Infrastructure.Repositories;

public class TableRepository : ITableRepository
{
    private readonly IMongoCollection<Table> _collection;
    private readonly IMapper _mapper;

    public TableRepository(IRestaurantReservationDatabase database, IMapper mapper)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _collection = database.GetDatabase().GetCollection<Table>("Tables");
    }

    public async Task<IEnumerable<Domain.Models.Table>> GetAllAsync(string restaurantId, int pageSize, int pageNumber)
    {
        return _mapper.Map<IEnumerable<Domain.Models.Table>>(await _collection
            .AsQueryable()
            .Where(table => table.RestaurantId.Equals(restaurantId))
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync());
    }

    public async Task<Domain.Models.Table?> GetByIdAsync(string tableId)
    {
        return _mapper.Map<Domain.Models.Table>(await _collection
            .AsQueryable()
            .Where(table => table.Id.Equals(tableId))
            .FirstOrDefaultAsync());
    }

    public async Task CreateAsync(string restaurantId, Domain.Models.Table table)
    {
        var newTable = _mapper.Map<Table>(table);
        newTable.RestaurantId = restaurantId;
        await _collection.InsertOneAsync(newTable);
    }

    public async Task UpdateAsync(string tableId, Domain.Models.Table table)
    {
        var filter = Builders<Table>.Filter.Eq(t => t.Id, tableId);
        var oldData = await _collection.FindAsync(filter).Result.FirstAsync();
        await _collection.FindOneAndReplaceAsync(filter, _mapper.Map(table, oldData));
    }

    public async Task DeleteAsync(string tableId)
    {
        var filter = Builders<Table>.Filter.Eq(t => t.Id, tableId);
        await _collection.DeleteOneAsync(filter);
    }
}