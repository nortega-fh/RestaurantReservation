using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.Contracts.Responses.API;
using RestaurantReservation.API.Contracts.Responses.MenuItems;
using RestaurantReservation.API.Contracts.Responses.Reservations;
using RestaurantReservation.Domain.MenuItems;
using RestaurantReservation.Domain.Reservations;

namespace RestaurantReservation.API.Controllers;

[ApiController]
[Authorize]
[Route("/api/restaurants/{restaurantId}/reservations")]
public class RestaurantReservationController : ControllerBase
{
    private const int DefaultPageSize = 20;
    private const int DefaultPageNumber = 1;
    private readonly IMapper _mapper;
    private readonly IReservationService _reservationService;

    public RestaurantReservationController(IReservationService reservationService, IMapper mapper)
    {
        _reservationService = reservationService ?? throw new ArgumentNullException(nameof(reservationService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet]
    public async Task<IActionResult> GetAllRestaurantReservations([FromRoute] string restaurantId,
        [FromQuery] int pageSize = DefaultPageSize,
        [FromQuery] int pageNumber = DefaultPageNumber,
        [FromQuery] string? customer = null)
    {
        var reservations = await _reservationService.GetAllAsync(restaurantId, pageSize, pageNumber, customer);
        return Ok(new CollectionResponse<ReservationResponse>
        {
            Metadata = new ResponseMetadata(reservations.Count, pageSize, pageNumber),
            Items = _mapper.Map<List<ReservationResponse>>(reservations)
        });
    }

    [HttpGet("{reservationId}")]
    public async Task<IActionResult> GetReservationById(string restaurantId, string reservationId)
    {
        var reservation = await _reservationService.GetByIdAsync(restaurantId, reservationId);
        return reservation is null ? NotFound() : Ok(_mapper.Map<ReservationResponse>(reservation));
    }

    [HttpGet("{reservationId}/menu-items")]
    public async Task<IActionResult> GetReservationMenuItems([FromRoute] string restaurantId,
        [FromRoute] string reservationId,
        [FromQuery] int pageSize = DefaultPageSize,
        [FromQuery] int pageNumber = DefaultPageNumber,
        [FromQuery] string? orderBy = null)
    {
        if (!await _reservationService.ReservationsExistsWithRestaurant(restaurantId, reservationId))
        {
            return NotFound();
        }
        var isOrderProperty = Enum.TryParse<MenuItemOrderableProperties>(orderBy, true, out var orderProperty);
        if (orderBy is null)
        {
            isOrderProperty = true;
            orderProperty = MenuItemOrderableProperties.Name;
        }
        if (!isOrderProperty)
        {
            return BadRequest($"The list can't be ordered by the property {orderBy}");
        }
        var menuItems =
            await _reservationService.GetReservationMenuItemsAsync(reservationId, pageSize, pageNumber, orderProperty);
        return Ok(new CollectionResponse<MenuItemResponse>
        {
            Metadata = new ResponseMetadata(menuItems.Count, pageSize, pageNumber),
            Items = _mapper.Map<IEnumerable<MenuItemResponse>>(menuItems)
        });
    }

    [HttpDelete("{reservationId}")]
    public async Task<IActionResult> DeleteReservation(string restaurantId, string reservationId)
    {
        if (await _reservationService.GetByIdAsync(restaurantId, reservationId) is null)
        {
            return NotFound();
        }
        await _reservationService.DeleteAsync(reservationId);
        return NoContent();
    }
}