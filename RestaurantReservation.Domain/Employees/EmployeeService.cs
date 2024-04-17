namespace RestaurantReservation.Domain.Employees;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _repository;

    public EmployeeService(IEmployeeRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<IEnumerable<Employee>> GetAllAsync(int pageSize, int pageNumber, string? role)
    {
        return await _repository.GetAllAsync(pageSize, pageNumber, role);
    }

    public async Task<Employee?> GetByIdAsync(string employeeId)
    {
        return await _repository.GetByIdAsync(employeeId);
    }

    public async Task CreateAsync(Employee employee)
    {
        await _repository.CreateAsync(employee);
    }

    public async Task UpdateAsync(Employee employee)
    {
        await _repository.UpdateAsync(employee);
    }

    public async Task DeleteAsync(string employeeId)
    {
        await _repository.DeleteAsync(employeeId);
    }

    public async Task<bool> ExistsWithIdAsync(string employeeId)
    {
        return await GetByIdAsync(employeeId) is not null;
    }
}