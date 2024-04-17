namespace RestaurantReservation.Domain.Employees;

public interface IEmployeeRepository
{
    Task<IEnumerable<Employee>> GetAllAsync(int pageSize, int pageNumber, string? role);
    Task<Employee?> GetByIdAsync(string employeeId);
    Task CreateAsync(Employee employee);
    Task UpdateAsync(Employee employee);
    Task DeleteAsync(string employeeId);
}