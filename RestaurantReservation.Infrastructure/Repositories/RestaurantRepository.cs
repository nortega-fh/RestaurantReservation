using MongoDB.Driver;
using MongoDB.Driver.Linq;
using RestaurantReservation.Domain.Restaurants;

namespace RestaurantReservation.Infrastructure.Repositories;

public class RestaurantRepository : IRestaurantRepository
{
    private readonly IMongoCollection<Restaurant> _collection;

    public RestaurantRepository(IRestaurantReservationDatabase database)
    {
        _collection = database.GetDatabase().GetCollection<Restaurant>("Restaurants");
    }

    public async Task<IEnumerable<Restaurant>> GetAllAsync(int pageSize, int pageNumber)
    {
        return await _collection
            .AsQueryable()
            .Where(_ => true)
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<Restaurant?> GetByIdAsync(string id)
    {
        return await _collection
            .AsQueryable()
            .Where(customer => customer.Id.Equals(id))
            .FirstOrDefaultAsync();
    }

    public async Task CreateAsync(Restaurant restaurant)
    {
        await _collection.InsertOneAsync(restaurant);
    }

    public async Task UpdateAsync(Restaurant restaurant)
    {
        var filter = Builders<Restaurant>.Filter.Eq(r => r.Id, restaurant.Id);
        await _collection.FindOneAndReplaceAsync(filter, restaurant);
    }

    public async Task DeleteAsync(string id)
    {
        await _collection.FindOneAndDeleteAsync(Builders<Restaurant>.Filter.Eq(restaurant => restaurant.Id, id));
    }
}