using RestaurantReservation.Domain.Users;

namespace RestaurantReservation.Domain.Customers;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _repository;
    private readonly IUserService _userService;

    public CustomerService(ICustomerRepository repository, IUserService userService)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
    }

    public async Task<IEnumerable<Customer>> GetAllAsync(int pageNumber, int pageSize)
    {
        return await _repository.GetAllAsync(pageNumber, pageSize);
    }

    public async Task<Customer?> GetByIdAsync(string id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<Customer?> CreateAsync(Customer customer)
    {
        return await _repository.CreateAsync(customer);
    }

    public async Task UpdateAsync(Customer customer)
    {
        await _repository.UpdateAsync(customer);
    }

    public async Task DeleteAsync(string id)
    {
        await _repository.DeleteAsync(id);
    }

    public async Task<bool> CustomerExistsWithIdAsync(string id)
    {
        return await GetByIdAsync(id) is not null;
    }

    public async Task<bool> UserExistsWithUsernameAsync(string username)
    {
        return await _userService.UserExistsAsync(username);
    }
}