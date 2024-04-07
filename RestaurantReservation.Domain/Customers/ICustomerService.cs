namespace RestaurantReservation.Domain.Customers;

public interface ICustomerService
{
    Task<IEnumerable<Customer>> GetAllAsync(int pageNumber, int pageSize);
    Task<Customer?> GetByIdAsync(string id);
    Task<Customer?> CreateAsync(Customer customer);
    Task UpdateAsync(string id, Customer customer);
    Task DeleteAsync(string id);
    Task<bool> CustomerExistsWithIdAsync(string id);
    Task<bool> UserExistsWithUsernameAsync(string username);
}