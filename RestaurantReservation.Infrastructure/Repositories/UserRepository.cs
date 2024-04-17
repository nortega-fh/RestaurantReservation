using MongoDB.Driver;
using RestaurantReservation.Domain.Users;

namespace RestaurantReservation.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IMongoCollection<User> _collection;

    public UserRepository(IRestaurantReservationDatabase database)
    {
        _collection = database.GetDatabase().GetCollection<User>("Users");
    }

    public async Task<User> CreateUserAsync(User user)
    {
        await _collection.InsertOneAsync(user);
        return await _collection
            .FindAsync(Builders<User>.Filter.Eq(u => u.Username, user.Username))
            .Result
            .FirstAsync();
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await _collection
            .FindAsync(Builders<User>.Filter.Eq(user => user.Username, username)).Result
            .FirstOrDefaultAsync();
    }
}