using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.Contracts.Responses;
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
        [FromQuery] int pageSize = DefaultPageSize, [FromQuery] int pageNumber = DefaultPageNumber)
    {
        var reservations = await _reservationService.GetAllAsync(restaurantId, pageSize, pageNumber);
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