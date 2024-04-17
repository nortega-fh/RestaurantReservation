using RestaurantReservation.Domain.Orders;

namespace RestaurantReservation.Domain.Employees;

public class EmployeeService : IEmployeeService
{
    private readonly IOrderService _orderService;
    private readonly IEmployeeRepository _repository;

    public EmployeeService(IEmployeeRepository repository, IOrderService orderService)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
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

    public async Task<decimal> GetEmployeeAverageOrderAmountAsync(string employeeId)
    {
        return await _orderService.GetAverageOrderAmountByEmployeeAsync(employeeId);
    }
}