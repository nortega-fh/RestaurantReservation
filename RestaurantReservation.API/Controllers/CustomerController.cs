using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.Contracts.Requests;
using RestaurantReservation.API.Contracts.Responses;
using RestaurantReservation.Domain.Models;
using RestaurantReservation.Domain.Services;

namespace RestaurantReservation.API.Controllers;

[ApiController]
[Route("/api/customers")]
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
        var response = new CollectionResponse<Customer>();
        var items = (await _customerService.GetAllAsync(pageNumber, pageSize)).ToList();
        response.Metadata = new ResponseMetadata(items.Count, pageSize, pageNumber);
        response.Items = items;
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCustomer(CustomerCreate createdCustomer)
    {
        var customer = _mapper.Map<Customer>(createdCustomer);
        await _customerService.CreateAsync(customer);
        return Created(Request.Path, customer);
    }

    [HttpDelete("{customerId}")]
    public async Task<IActionResult> DeleteCustomer(string customerId)
    {
        await _customerService.DeleteAsync(customerId);
        return NoContent();
    }
}