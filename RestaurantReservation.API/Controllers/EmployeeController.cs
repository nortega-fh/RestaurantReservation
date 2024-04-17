using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.Contracts.Requests.Employees;
using RestaurantReservation.API.Contracts.Responses.API;
using RestaurantReservation.API.Contracts.Responses.Employees;
using RestaurantReservation.Domain.Employees;
using RestaurantReservation.Domain.Restaurants;
using RestaurantReservation.Domain.Users;

namespace RestaurantReservation.API.Controllers;

[ApiController]
[Authorize]
[Route("/api/restaurants/{restaurantId}/employees")]
public class EmployeeController : ControllerBase
{
    private const int DefaultPageSize = 20;
    private const int DefaultPageNumber = 1;
    private readonly IEmployeeService _employeeService;
    private readonly IMapper _mapper;
    private readonly IRestaurantService _restaurantService;
    private readonly IUserService _userService;

    public EmployeeController(IRestaurantService restaurantService, IEmployeeService service, IUserService userService,
        IMapper mapper)
    {
        _restaurantService = restaurantService ?? throw new ArgumentNullException(nameof(restaurantService));
        _employeeService = service ?? throw new ArgumentNullException(nameof(service));
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet]
    public async Task<IActionResult> GetAllEmployees([FromRoute] string restaurantId,
        [FromQuery] int pageSize = DefaultPageSize,
        [FromQuery] int pageNumber = DefaultPageNumber,
        [FromQuery] string? role = null)
    {
        if (!await _restaurantService.RestaurantExistsWithIdAsync(restaurantId))
        {
            return NotFound("Restaurant not found");
        }
        var employees = _mapper
            .Map<IEnumerable<EmployeeResponse>>(await _employeeService.GetAllAsync(pageSize, pageNumber, role))
            .ToList();
        return Ok(new CollectionResponse<EmployeeResponse>
        {
            Metadata = new ResponseMetadata(employees.Count, pageSize, pageNumber),
            Items = employees
        });
    }

    [HttpGet("{employeeId}")]
    public async Task<IActionResult> GetEmployeeById([FromRoute] string restaurantId, string employeeId)
    {
        if (!await _restaurantService.RestaurantExistsWithIdAsync(restaurantId))
        {
            return NotFound("Restaurant not found");
        }
        var employee = await _employeeService.GetByIdAsync(employeeId);
        return employee is null ? NotFound() : Ok(_mapper.Map<EmployeeResponse>(employee));
    }

    [HttpPost]
    public async Task<IActionResult> CreateEmployee([FromRoute] string restaurantId,
        [FromBody] EmployeeCreate employeeCreate)
    {
        if (!await _restaurantService.RestaurantExistsWithIdAsync(restaurantId))
        {
            return NotFound("Restaurant not found");
        }
        if (!await _userService.UserExistsAsync(employeeCreate.Username))
        {
            return NotFound($"User with username {employeeCreate.Username} not found");
        }
        var employee = _mapper.Map<Employee>(employeeCreate);
        employee.RestaurantId = restaurantId;
        await _employeeService.CreateAsync(employee);
        return Created(Request.Path, null);
    }

    [HttpPut("{employeeId}")]
    public async Task<IActionResult> UpdateEmployee([FromRoute] string restaurantId, string employeeId,
        [FromBody] EmployeeUpdate employeeUpdate)
    {
        if (!await _restaurantService.RestaurantExistsWithIdAsync(restaurantId))
        {
            return NotFound("Restaurant not found");
        }
        var employee = await _employeeService.GetByIdAsync(employeeId);
        if (employee is null)
        {
            return NotFound($"Employee with id {employeeId} not found");
        }
        _mapper.Map(employeeUpdate, employee);
        await _employeeService.UpdateAsync(employee);
        return NoContent();
    }

    [HttpDelete("{employeeId}")]
    public async Task<IActionResult> DeleteEmployee([FromRoute] string restaurantId, string employeeId)
    {
        if (!await _restaurantService.RestaurantExistsWithIdAsync(restaurantId))
        {
            return NotFound("Restaurant not found");
        }
        if (!await _employeeService.ExistsWithIdAsync(employeeId))
        {
            return NotFound($"Employee with id {employeeId} not found");
        }
        await _employeeService.DeleteAsync(employeeId);
        return NoContent();
    }
}