using RestaurantReservation.API.Entities;
using RestaurantReservation.API.Repositories;

namespace RestaurantReservation.API.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _repository;

    public CustomerService(ICustomerRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<IEnumerable<Customer>> GetAllAsync(int pageNumber, int pageSize)
    {
        return await _repository.GetAllAsync(pageNumber, pageSize);
    }

    public async Task<Customer?> GetByIdAsync(string id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task CreateAsync(Customer customer)
    {
        await _repository.CreateAsync(customer);
    }

    public async Task UpdateAsync(string id, Customer customer)
    {
        await _repository.UpdateAsync(id, customer);
    }

    public async Task DeleteAsync(string id)
    {
        await _repository.DeleteAsync(id);
    }
}