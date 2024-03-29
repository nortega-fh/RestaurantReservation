using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MongoDB.Driver;
using RestaurantReservation.Domain.Repositories;
using RestaurantReservation.Infrastructure.Entities;

namespace RestaurantReservation.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IMongoCollection<User> _collection;
    private readonly IMapper _mapper;

    public UserRepository(IRestaurantReservationDatabase database, IMapper mapper)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _collection = database.GetDatabase().GetCollection<User>("Users");
    }

    public async Task DeleteAsync(string id)
    {
        await _collection.DeleteOneAsync(Builders<User>.Filter.Eq(user => user.Id, id));
    }

    public async Task<Domain.Models.User> CreateUserAsync(Domain.Models.User user)
    {
        user.Password = new PasswordHasher<Domain.Models.User>().HashPassword(user, user.Password);
        await _collection.InsertOneAsync(_mapper.Map<User>(user));
        return _mapper.Map<Domain.Models.User>(await _collection
            .FindAsync(Builders<User>.Filter.Eq(u => u.Username, user.Username))
            .Result
            .FirstAsync());
    }

    public async Task<Domain.Models.User?> GetByUsernameAsync(string username)
    {
        return _mapper.Map<Domain.Models.User>(await _collection
            .FindAsync(Builders<User>.Filter.Eq(user => user.Username, username)).Result
            .FirstAsync());
    }

    public async Task UpdateAsync(string id, Domain.Models.User user)
    {
        var findById = Builders<User>.Filter.Eq(u => u.Id, id);
        var oldUser = await _collection.FindAsync(findById).Result.FirstAsync();
        var newUser = _mapper.Map(user, oldUser);
        await _collection.ReplaceOneAsync(findById, newUser);
    }
}