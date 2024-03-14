using MongoDB.Driver;
using RestaurantReservation.API.Entities;
using RestaurantReservation.API.Settings;

namespace RestaurantReservation.API.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly IMongoCollection<Customer> _customerCollection;
    
    public CustomerRepository(IRestaurantReservationDatabase database)
    {
        _customerCollection = database.GetDatabase().GetCollection<Customer>("Customers");
    }

    public async Task<IEnumerable<Customer>> GetAllAsync() => await _customerCollection.Find(_ => true).ToListAsync();

    public async Task<Customer> GetByIdAsync(string id) =>
        await _customerCollection.Find(customer => customer.Id.Equals(id)).FirstAsync();
    

    public async Task CreateAsync(Customer customer)
    {
        await _customerCollection.InsertOneAsync(customer);
    }

    public async Task UpdateAsync(string id, Customer customer)
    {
        var findById = Builders<Customer>.Filter.Eq(cust => cust.Id, id);
        await _customerCollection.ReplaceOneAsync(findById, customer);
    }

    public async Task DeleteAsync(string id)
    {
        await _customerCollection.DeleteOneAsync(customer => customer.Id.Equals(id));
    }
}