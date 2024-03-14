using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.Services;

namespace RestaurantReservation.API.Controllers;

[ApiController]
[Route("/api/customers")]
public class CustomerController : ControllerBase
{
   private readonly ICustomerService _customerService;

   public CustomerController(ICustomerService customerService)
   {
      _customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
   }
   
   [HttpGet] 
   public async Task<IActionResult> GetAllCustomers()
   {
      return Ok(await _customerService.GetAllAsync());
   } 
}