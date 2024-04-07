﻿using MongoDB.Driver;
using RestaurantReservation.Domain.Users;

namespace RestaurantReservation.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IMongoCollection<User> _collection;

    public UserRepository(IRestaurantReservationDatabase database)
    {
        _collection = database.GetDatabase().GetCollection<User>("Users");
    }

    public async Task DeleteAsync(string id)
    {
        await _collection.DeleteOneAsync(Builders<User>.Filter.Eq(user => user.Id, id));
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

    public async Task UpdateAsync(string id, User user)
    {
        var findById = Builders<User>.Filter.Eq(u => u.Id, id);
        await _collection.ReplaceOneAsync(findById, user);
    }
}