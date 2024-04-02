using AutoMapper;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using RestaurantReservation.Domain.Repositories;
using RestaurantReservation.Infrastructure.Entities;

namespace RestaurantReservation.Infrastructure.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly IMongoCollection<Customer> _collection;
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    public CustomerRepository(IRestaurantReservationDatabase database, IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _collection = database.GetDatabase().GetCollection<Customer>("Customers");
    }

    public async Task<IEnumerable<Domain.Models.Customer>> GetAllAsync(int pageNumber, int pageSize)
    {
        return _mapper.Map<IEnumerable<Domain.Models.Customer>>(await _collection.AsQueryable()
            .Where(_ => true)
            .Skip(pageNumber * pageSize - pageSize)
            .Take(pageSize)
            .ToListAsync());
    }

    public async Task<Domain.Models.Customer?> GetByIdAsync(string id)
    {
        return _mapper.Map<Domain.Models.Customer>(await _collection.AsQueryable()
            .Where(customer => customer.Id.Equals(id))
            .FirstOrDefaultAsync());
    }

    public async Task<Domain.Models.Customer?> CreateAsync(Domain.Models.Customer customer)
    {
        var relatedUser = await _userRepository.GetByUsernameAsync(customer.Username);
        if (relatedUser is null)
        {
            return null;
        }
        var customerEntity = _mapper.Map<Customer>(customer);
        _mapper.Map(relatedUser, customerEntity);
        customerEntity.UserId = relatedUser.Id;
        await _collection.InsertOneAsync(customerEntity);
        return _mapper.Map<Domain.Models.Customer>(await _collection
            .FindAsync(Builders<Customer>.Filter.Eq(cust => cust.Username, customer.Username))
            .Result
            .FirstAsync());
    }

    public async Task UpdateAsync(string id, Domain.Models.Customer customer)
    {
        var filter = Builders<Customer>.Filter.Eq(c => c.Id, id);
        var oldData = await _collection.FindAsync(filter).Result.FirstAsync();
        var newData = _mapper.Map(customer, oldData);
        await _collection.FindOneAndReplaceAsync(filter, newData);
    }

    public async Task DeleteAsync(string id)
    {
        await _collection.FindOneAndDeleteAsync(Builders<Customer>.Filter.Eq(customer => customer.Id, id));
    }
}