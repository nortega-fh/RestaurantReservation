namespace RestaurantReservation.Domain.Customers;

public interface ICustomerRepository
{
    Task<IEnumerable<Customer>> GetAllAsync(int pageNumber, int pageSize);
    Task<Customer?> GetByIdAsync(string id);
    Task<Customer?> CreateAsync(Customer customer);
    Task UpdateAsync(Customer customer);
    Task DeleteAsync(string id);
}