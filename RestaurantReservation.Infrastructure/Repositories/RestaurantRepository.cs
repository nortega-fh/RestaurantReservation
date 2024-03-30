using AutoMapper;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using RestaurantReservation.Domain.Repositories;
using RestaurantReservation.Infrastructure.Entities;

namespace RestaurantReservation.Infrastructure.Repositories;

public class RestaurantRepository : IRestaurantRepository
{
    private readonly IMongoCollection<Restaurant> _collection;
    private readonly IMapper _mapper;

    public RestaurantRepository(IRestaurantReservationDatabase database, IMapper mapper)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _collection = database.GetDatabase().GetCollection<Restaurant>("Restaurants");
    }

    public async Task<IEnumerable<Domain.Models.Restaurant>> GetAllAsync(int pageSize, int pageNumber)
    {
        return _mapper.Map<IEnumerable<Domain.Models.Restaurant>>(await _collection
            .AsQueryable()
            .Where(_ => true)
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync());
    }

    public async Task<Domain.Models.Restaurant?> GetByIdAsync(string id)
    {
        return _mapper.Map<Domain.Models.Restaurant>(await _collection
            .AsQueryable()
            .Where(customer => customer.Id.Equals(id))
            .FirstAsync());
    }

    public async Task<Domain.Models.Restaurant?> CreateAsync(Domain.Models.Restaurant restaurant)
    {
        await _collection.InsertOneAsync(_mapper.Map<Restaurant>(restaurant));
        return _mapper.Map<Domain.Models.Restaurant>(await _collection
            .FindAsync(Builders<Restaurant>.Filter.Eq(rest => rest.Name, restaurant.Name))
            .Result
            .FirstAsync());
    }

    public async Task UpdateAsync(string id, Domain.Models.Restaurant restaurant)
    {
        var filter = Builders<Restaurant>.Filter.Eq(r => r.Id, id);
        var oldData = await _collection.FindAsync(filter).Result.FirstAsync();
        var newData = _mapper.Map(restaurant, oldData);
        await _collection.FindOneAndReplaceAsync(filter, newData);
    }

    public async Task DeleteAsync(string id)
    {
        await _collection.FindOneAndDeleteAsync(Builders<Restaurant>.Filter.Eq(restaurant => restaurant.Id, id));
    }
}