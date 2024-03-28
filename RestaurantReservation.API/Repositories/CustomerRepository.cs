using MongoDB.Driver;
using MongoDB.Driver.Linq;
using RestaurantReservation.API.Entities;
using RestaurantReservation.API.Settings;

namespace RestaurantReservation.API.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly IMongoCollection<Customer> _collection;
    private readonly IUserRepository _userRepository;

    public CustomerRepository(IRestaurantReservationDatabase database, IUserRepository userRepository)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _collection = database.GetDatabase().GetCollection<Customer>("Customers");
    }

    public async Task<IEnumerable<Customer>> GetAllAsync(int pageNumber, int pageSize)
    {
        return await _collection.AsQueryable()
            .Where(_ => true)
            .Skip(pageNumber * pageSize - pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<Customer?> GetByIdAsync(string id)
    {
        return await _collection.AsQueryable()
            .Where(customer => customer.Id.Equals(id))
            .FirstAsync();
    }

    public async Task CreateAsync(Customer customer)
    {
        var relatedUser = await _userRepository.GetByUsernameAsync(customer.Username);
        if (relatedUser is null)
        {
            return;
        }
        customer.UserId = relatedUser.Id;
        await _collection.InsertOneAsync(customer);
    }

    public async Task UpdateAsync(string id, Customer customer)
    {
        var filter = Builders<Customer>.Filter.Eq(c => c.Id, id);
        await _collection.FindOneAndReplaceAsync(filter, customer);
    }

    public async Task DeleteAsync(string id)
    {
        await _collection.FindOneAndDeleteAsync(Builders<Customer>.Filter.Eq(customer => customer.Id, id));
    }
}