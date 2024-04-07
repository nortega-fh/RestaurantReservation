using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.Contracts.Requests;
using RestaurantReservation.API.Contracts.Responses;
using RestaurantReservation.Domain.Customers;

namespace RestaurantReservation.API.Controllers;

[ApiController]
[Route("/api/customers")]
[Authorize]
public class CustomerController : ControllerBase
{
    private const int DefaultPageSize = 20;
    private const int DefaultPageNumber = 1;
    private readonly ICustomerService _customerService;
    private readonly IMapper _mapper;

    public CustomerController(ICustomerService customerService, IMapper mapper)
    {
        _customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCustomers(int pageNumber = DefaultPageNumber, int pageSize = DefaultPageSize)
    {
        var items = _mapper.Map<List<CustomerResponse>>(await _customerService.GetAllAsync(pageNumber, pageSize));
        return Ok(new CollectionResponse<CustomerResponse>
        {
            Metadata = new ResponseMetadata(items.Count, pageSize, pageNumber),
            Items = items
        });
    }

    [HttpGet("{customerId}")]
    public async Task<IActionResult> GetCustomerById(string customerId)
    {
        var customer = await _customerService.GetByIdAsync(customerId);
        return customer is null ? NotFound() : Ok(_mapper.Map<CustomerResponse>(customer));
    }

    [HttpPost]
    public async Task<IActionResult> CreateCustomer(CustomerCreate createdCustomer)
    {
        if (!await _customerService.UserExistsWithUsernameAsync(createdCustomer.Username))
        {
            return NotFound($"Username {createdCustomer.Username} doesn't exist");
        }
        var customer = await _customerService.CreateAsync(_mapper.Map<Customer>(createdCustomer));
        return Created(Request.Path, _mapper.Map<CustomerResponse>(customer));
    }

    [HttpPut("{customerId}")]
    public async Task<IActionResult> UpdateCustomer(string customerId, CustomerUpdate updatedCustomer)
    {
        var customer = await _customerService.GetByIdAsync(customerId);
        if (customer is null)
        {
            return NotFound();
        }
        _mapper.Map(updatedCustomer, customer);
        await _customerService.UpdateAsync(customer);
        return NoContent();
    }

    [HttpDelete("{customerId}")]
    public async Task<IActionResult> DeleteCustomer(string customerId)
    {
        if (!await _customerService.CustomerExistsWithIdAsync(customerId))
        {
            return NotFound();
        }
        await _customerService.DeleteAsync(customerId);
        return NoContent();
    }
}