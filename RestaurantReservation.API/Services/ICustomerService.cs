using RestaurantReservation.API.Entities;

namespace RestaurantReservation.API.Services;

public interface ICustomerService
{
   Task<IEnumerable<Customer>> GetAllAsync();
   Task<Customer> GetByIdAsync(string id);
   Task CreateAsync(Customer customer);
   Task UpdateAsync(string id, Customer customer);
   Task DeleteAsync(string id);
}