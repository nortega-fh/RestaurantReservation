using Microsoft.AspNetCore.Identity;
using MongoDB.Driver;
using RestaurantReservation.API.Entities;
using RestaurantReservation.API.Settings;

namespace RestaurantReservation.API.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IMongoCollection<User> _collection;

    public UserRepository(IRestaurantReservationDatabase database)
    {
        _collection = database.GetDatabase().GetCollection<User>("Users");
    }

    public async Task<User> CreateUserAsync(User user)
    {
        user.Password = new PasswordHasher<User>().HashPassword(user, user.Password);
        await _collection.InsertOneAsync(user);
        return await _collection.FindAsync(Builders<User>.Filter.Eq(u => u.Username, user.Username))
            .Result
            .FirstAsync();
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await _collection.FindAsync(Builders<User>.Filter.Eq(user => user.Username, username)).Result
            .FirstAsync();
    }

    public async Task UpdateAsync(string id, User user)
    {
        var findById = Builders<User>.Filter.Eq(u => u.Id, id);
        await _collection.ReplaceOneAsync(findById, user);
    }

    public async Task DeleteAsync(string id)
    {
        await _collection.DeleteOneAsync(Builders<User>.Filter.Eq(user => user.Id, id));
    }
}