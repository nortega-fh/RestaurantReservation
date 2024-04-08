using MongoDB.Driver;
using MongoDB.Driver.Linq;
using RestaurantReservation.Domain.MenuItems;

namespace RestaurantReservation.Infrastructure.Repositories;

public class MenuItemRepository : IMenuItemRepository
{
    private readonly IMongoCollection<MenuItem> _collection;

    public MenuItemRepository(IRestaurantReservationDatabase database)
    {
        _collection = database.GetDatabase().GetCollection<MenuItem>("MenuItems");
    }

    public async Task<IEnumerable<MenuItem>> GetAllAsync(string restaurantId, int pageSize, int pageNumber)
    {
        return await _collection.AsQueryable()
            .Where(item => item.RestaurantId.Equals(restaurantId))
            .Skip(pageNumber * pageSize - pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<MenuItem?> GetByIdAsync(string restaurantId, string menuItemId)
    {
        return await _collection.AsQueryable()
            .Where(menuItem => menuItem.Id.Equals(menuItemId) && menuItem.RestaurantId.Equals(restaurantId))
            .FirstOrDefaultAsync();
    }

    public async Task CreateAsync(MenuItem menuItem)
    {
        await _collection.InsertOneAsync(menuItem);
    }

    public async Task UpdateAsync(MenuItem menuItem)
    {
        var findById = Builders<MenuItem>.Filter.Eq(mI => mI.Id, menuItem.Id);
        await _collection.FindOneAndReplaceAsync(findById, menuItem);
    }

    public async Task DeleteAsync(string menuItemId)
    {
        var findById = Builders<MenuItem>.Filter.Eq(menuItem => menuItem.Id, menuItemId);
        await _collection.FindOneAndDeleteAsync(findById);
    }
}