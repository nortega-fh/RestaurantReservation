using MongoDB.Driver;
using MongoDB.Driver.Linq;
using RestaurantReservation.Domain.Employees;
using RestaurantReservation.Domain.Users;

namespace RestaurantReservation.Infrastructure.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly IMongoCollection<Employee> _collection;
    private readonly IUserRepository _userRepository;

    public EmployeeRepository(IRestaurantReservationDatabase database, IUserRepository userRepository)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _collection = database.GetDatabase().GetCollection<Employee>("Employees");
    }

    public async Task<IEnumerable<Employee>> GetAllAsync(int pageSize, int pageNumber)
    {
        return await _collection.AsQueryable()
            .Where(_ => true)
            .Skip(pageNumber * pageSize - pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<Employee?> GetByIdAsync(string employeeId)
    {
        return await _collection.AsQueryable()
            .Where(employee => employee.Id.Equals(employeeId))
            .FirstOrDefaultAsync();
    }

    public async Task CreateAsync(Employee employee)
    {
        var relatedUser = await _userRepository.GetByUsernameAsync(employee.Username);
        if (relatedUser is null)
        {
            return;
        }
        employee.FirstName = relatedUser.FirstName;
        employee.LastName = relatedUser.LastName;
        employee.Password = relatedUser.Password;
        await _collection.InsertOneAsync(employee);
    }

    public async Task UpdateAsync(Employee employee)
    {
        var filter = Builders<Employee>.Filter.Eq(c => c.Id, employee.Id);
        await _collection.FindOneAndReplaceAsync(filter, employee);
    }

    public async Task DeleteAsync(string employeeId)
    {
        await _collection.FindOneAndDeleteAsync(Builders<Employee>.Filter.Eq(employee => employee.Id, employeeId));
    }
}