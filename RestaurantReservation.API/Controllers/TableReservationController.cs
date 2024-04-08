using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.Contracts.Requests;
using RestaurantReservation.API.Contracts.Responses;
using RestaurantReservation.Domain.Reservations;
using RestaurantReservation.Domain.Restaurants;
using RestaurantReservation.Domain.Tables;

namespace RestaurantReservation.API.Controllers;

[ApiController]
[Authorize]
[Route("/api/restaurants/{restaurantId}/tables/{tableId}/reservations")]
public class TableReservationController : ControllerBase
{
    private const int DefaultPageSize = 20;
    private const int DefaultPageNumber = 1;
    private readonly IMapper _mapper;
    private readonly IReservationService _reservationService;
    private readonly IRestaurantService _restaurantService;
    private readonly ITableService _tableService;

    public TableReservationController(IRestaurantService restaurantService, ITableService tableService,
        IReservationService reservationService, IMapper mapper)
    {
        _restaurantService = restaurantService ?? throw new ArgumentNullException(nameof(restaurantService));
        _tableService = tableService ?? throw new ArgumentNullException(nameof(tableService));
        _reservationService = reservationService ?? throw new ArgumentNullException(nameof(reservationService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTableReservations([FromRoute] string restaurantId,
        [FromRoute] string tableId, [FromQuery] int pageSize = DefaultPageSize,
        [FromQuery] int pageNumber = DefaultPageNumber)
    {
        var reservations = await _reservationService.GetAllAsync(restaurantId, tableId, pageSize, pageNumber);
        return Ok(new CollectionResponse<ReservationResponse>
        {
            Metadata = new ResponseMetadata(reservations.Count, pageSize, pageNumber),
            Items = _mapper.Map<List<ReservationResponse>>(reservations)
        });
    }

    [HttpGet("{reservationId}")]
    public async Task<IActionResult> GetTableReservationById(string restaurantId, string tableId, string reservationId)
    {
        var reservation = await _reservationService.GetByIdAsync(restaurantId, tableId, reservationId);
        return reservation is null ? NotFound() : Ok(_mapper.Map<ReservationResponse>(reservation));
    }

    [HttpPost]
    public async Task<IActionResult> CreateReservation([FromRoute] string restaurantId, [FromRoute] string tableId,
        [FromBody] ReservationCreate reservationCreate)
    {
        if (!await _tableService.ExistsWithIdAsync(tableId) ||
            !await _restaurantService.RestaurantExistsWithIdAsync(restaurantId))
        {
            return NotFound();
        }
        var reservation = _mapper.Map<Reservation>(reservationCreate);
        reservation.RestaurantId = restaurantId;
        reservation.TableId = tableId;
        await _reservationService.CreateAsync(reservation);
        return Created(Request.Path, null);
    }

    [HttpPut("{reservationId}")]
    public async Task<IActionResult> UpdateReservation([FromRoute] string restaurantId, [FromRoute] string tableId,
        [FromRoute] string reservationId, [FromBody] ReservationCreate reservationCreate)
    {
        var reservation = await _reservationService.GetByIdAsync(restaurantId, tableId, reservationId);
        if (reservation is null)
        {
            return NotFound();
        }
        _mapper.Map(reservationCreate, reservation);
        await _reservationService.UpdateAsync(reservation);
        return NoContent();
    }

    [HttpDelete("{reservationId}")]
    public async Task<IActionResult> DeleteReservation(string restaurantId, string tableId, string reservationId)
    {
        if (await _reservationService.GetByIdAsync(restaurantId, tableId, reservationId) is null)
        {
            return NotFound();
        }
        await _reservationService.DeleteAsync(reservationId);
        return NoContent();
    }
}