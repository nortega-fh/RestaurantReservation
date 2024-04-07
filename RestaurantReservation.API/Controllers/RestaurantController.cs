using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.Contracts.Requests;
using RestaurantReservation.API.Contracts.Responses;
using RestaurantReservation.Domain.Restaurants;

namespace RestaurantReservation.API.Controllers;

[ApiController]
[Authorize]
[Route("/api/restaurants")]
public class RestaurantController : ControllerBase
{
    private const int DefaultPageSize = 20;
    private const int DefaultPageNumber = 1;
    private readonly IMapper _mapper;
    private readonly IRestaurantService _service;

    public RestaurantController(IRestaurantService service, IMapper mapper)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(int pageSize = DefaultPageSize, int pageNumber = DefaultPageNumber)
    {
        return Ok(await _service.GetAllAsync(pageSize, pageNumber));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var restaurant = _mapper.Map<RestaurantResponse?>(await _service.GetByIdAsync(id));
        return restaurant is null ? NotFound() : Ok(restaurant);
    }

    [HttpPost]
    public async Task<IActionResult> Create(RestaurantCreate restaurantCreate)
    {
        await _service.CreateAsync(_mapper.Map<Restaurant>(restaurantCreate));
        return Created(Request.Path, null);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, RestaurantCreate restaurantUpdate)
    {
        if (!await _service.RestaurantExistsWithIdAsync(id))
        {
            return NotFound();
        }
        var restaurant = _mapper.Map<Restaurant>(restaurantUpdate);
        restaurant.Id = id;
        await _service.UpdateAsync(id, restaurant);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        if (!await _service.RestaurantExistsWithIdAsync(id))
        {
            return NotFound();
        }
        await _service.DeleteAsync(id);
        return NoContent();
    }
}