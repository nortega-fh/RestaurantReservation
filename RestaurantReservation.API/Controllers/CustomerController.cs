using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.Dtos.Requests;
using RestaurantReservation.API.Dtos.Responses;
using RestaurantReservation.API.Entities;
using RestaurantReservation.API.Services;

namespace RestaurantReservation.API.Controllers;

[ApiController]
[Route("/api/customers")]
public class CustomerController : ControllerBase
{
   private readonly ICustomerService _customerService;
   private readonly IMapper _mapper;
   private readonly IValidator<CustomerCreateDto> _validator;
   private const int DefaultPageSize = 20;
   private const int DefaultPageNumber = 1;

   public CustomerController(ICustomerService customerService, IMapper mapper, IValidator<CustomerCreateDto> validator)
   {
      _customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
      _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
      _validator = validator ?? throw new ArgumentNullException(nameof(validator));
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
   public async Task<IActionResult> CreateCustomer(CustomerCreateDto createdCustomer)
   {
      var validationResult = _validator.Validate(createdCustomer);
      if (!validationResult.IsValid)
      {
         var errorResponse = new ErrorResponse
         {
             RequestPath = HttpContext.Request.Path,
             Errors = validationResult.ToDictionary()
         };
         return BadRequest(errorResponse);
      }
      var customer = _mapper.Map<Customer>(createdCustomer);
      await _customerService.CreateAsync(customer);
      return Ok(customer);
   }

   [HttpDelete("{customerId}")]
   public async Task<IActionResult> DeleteCustomer(string customerId)
   {
      await _customerService.DeleteAsync(customerId);
      return NoContent();
   }
}