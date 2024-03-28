using RestaurantReservation.API.Entities;

namespace RestaurantReservation.API.Repositories;

public interface ICustomerRepository
{
    Task<IEnumerable<Customer>> GetAllAsync(int pageNumber, int pageSize);
    Task<Customer?> GetByIdAsync(string id);
    Task CreateAsync(Customer customer);
    Task UpdateAsync(string id, Customer customer);
    Task DeleteAsync(string id);
}