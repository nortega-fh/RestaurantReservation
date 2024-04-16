using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.Contracts.Requests;
using RestaurantReservation.API.Contracts.Responses;
using RestaurantReservation.Domain.Orders;
using RestaurantReservation.Domain.Reservations;
using RestaurantReservation.Domain.Restaurants;

namespace RestaurantReservation.API.Controllers;

[ApiController]
[Authorize]
[Route("/api/restaurants/{restaurantId}/reservations/{reservationId}/orders")]
public class RestaurantReservationOrderController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IOrderService _orderService;
    private readonly IReservationService _reservationService;
    private readonly IRestaurantService _restaurantService;

    public RestaurantReservationOrderController(IRestaurantService restaurantService,
        IReservationService reservationService, IOrderService orderService, IMapper mapper)
    {
        _restaurantService = restaurantService ?? throw new ArgumentNullException(nameof(restaurantService));
        _reservationService = reservationService ?? throw new ArgumentNullException(nameof(reservationService));
        _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet]
    public async Task<IActionResult> GetAllOrdersByReservation([FromRoute] string restaurantId,
        [FromRoute] string reservationId, [FromQuery] int pageSize, [FromQuery] int pageNumber)
    {
        if (!await _restaurantService.RestaurantExistsWithIdAsync(restaurantId) ||
            !await _reservationService.ReservationsExistsWithRestaurant(restaurantId, reservationId))
        {
            return NotFound();
        }
        var orders = await _orderService.GetAllOrdersByReservation(reservationId, pageSize, pageNumber);
        return Ok(new CollectionResponse<OrderReservationResponse>
        {
            Metadata = new ResponseMetadata(orders.Count, pageSize, pageNumber),
            Items = _mapper.Map<IEnumerable<OrderReservationResponse>>(orders)
        });
    }

    [HttpGet("{orderId}")]
    public async Task<IActionResult> GetOrderById(string restaurantId, string reservationId, string orderId)
    {
        if (!await _restaurantService.RestaurantExistsWithIdAsync(restaurantId) ||
            !await _reservationService.ReservationsExistsWithRestaurant(restaurantId, reservationId))
        {
            return NotFound();
        }
        var order = await _orderService.GetByIdAndReservationId(orderId, reservationId);
        return order is null ? NotFound() : Ok(_mapper.Map<OrderReservationResponse>(order));
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromRoute] string restaurantId, [FromRoute] string reservationId,
        [FromBody] OrderReservationCreate orderReservationCreate)
    {
        if (!await _restaurantService.RestaurantExistsWithIdAsync(restaurantId) ||
            !await _reservationService.ReservationsExistsWithRestaurant(restaurantId, reservationId))
        {
            return NotFound();
        }
        var order = new Order
        {
            ReservationId = reservationId,
            EmployeeId = orderReservationCreate.EmployeeId,
            OrderDate = DateTime.Now
        };
        await _orderService.Create(order);
        return Created(Request.Path, null);
    }

    [HttpPut("{orderId}")]
    public async Task<IActionResult> UpdateOrder([FromRoute] string restaurantId, [FromRoute] string reservationId,
        [FromRoute] string orderId, [FromBody] OrderReservationCreate orderReservationCreate)
    {
        if (!await _restaurantService.RestaurantExistsWithIdAsync(restaurantId) ||
            !await _reservationService.ReservationsExistsWithRestaurant(restaurantId, reservationId))
        {
            return NotFound();
        }
        var order = await _orderService.GetByIdAndReservationId(orderId, reservationId);
        if (order is null)
        {
            return NotFound();
        }
        _mapper.Map(orderReservationCreate, order);
        await _orderService.Update(orderId, order);
        return NoContent();
    }

    [HttpDelete("{orderId}")]
    public async Task<IActionResult> DeleteOrder(string restaurantId, string reservationId, string orderId)
    {
        if (!await _restaurantService.RestaurantExistsWithIdAsync(restaurantId) ||
            !await _orderService.OrderExistsWithReservation(reservationId, orderId))
        {
            return NotFound();
        }
        await _orderService.Delete(orderId);
        return NoContent();
    }
}