using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.Contracts.Requests;
using RestaurantReservation.API.Contracts.Responses;
using RestaurantReservation.Domain.Models;
using RestaurantReservation.Domain.Services;

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

    [HttpPost]
    public async Task<IActionResult> CreateCustomer(CustomerCreate createdCustomer)
    {
        var customer = await _customerService.CreateAsync(_mapper.Map<Customer>(createdCustomer));
        return Created(Request.Path, _mapper.Map<CustomerResponse>(customer));
    }

    [HttpDelete("{customerId}")]
    public async Task<IActionResult> DeleteCustomer(string customerId)
    {
        await _customerService.DeleteAsync(customerId);
        return NoContent();
    }
}