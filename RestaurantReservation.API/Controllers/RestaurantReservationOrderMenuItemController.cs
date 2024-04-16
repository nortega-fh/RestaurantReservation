using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.Contracts.Requests.Orders;
using RestaurantReservation.Domain.MenuItems;
using RestaurantReservation.Domain.Orders;
using RestaurantReservation.Domain.Reservations;
using RestaurantReservation.Domain.Restaurants;

namespace RestaurantReservation.API.Controllers;

[ApiController]
[Authorize]
[Route("/api/restaurants/{restaurantId}/reservations/{reservationId}/orders/{orderId}/items")]
public class RestaurantReservationOrderMenuItemController : ControllerBase
{
    private readonly IMenuItemService _menuItemService;
    private readonly IOrderService _orderService;
    private readonly IReservationService _reservationService;
    private readonly IRestaurantService _restaurantService;

    public RestaurantReservationOrderMenuItemController(IRestaurantService restaurantService,
        IReservationService reservationService, IOrderService orderService, IMenuItemService menuItemService)
    {
        _restaurantService = restaurantService ?? throw new ArgumentNullException(nameof(restaurantService));
        _reservationService = reservationService ?? throw new ArgumentNullException(nameof(reservationService));
        _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
        _menuItemService = menuItemService ?? throw new ArgumentNullException(nameof(menuItemService));
    }

    [HttpGet]
    public async Task<IActionResult> GetOrderMenuItems([FromRoute] string restaurantId,
        [FromRoute] string reservationId,
        [FromRoute] string orderId)
    {
        var order = await _orderService.GetByIdAndReservationId(orderId, reservationId);
        if (!await _restaurantService.RestaurantExistsWithIdAsync(restaurantId)
            || !await _reservationService.ReservationsExistsWithRestaurant(restaurantId, reservationId)
            || order is null)
        {
            return NotFound();
        }
        return Ok(order.Items);
    }

    [HttpPost]
    public async Task<IActionResult> AddOrderMenuItemToOrder([FromRoute] string restaurantId,
        [FromRoute] string reservationId, [FromRoute] string orderId, [FromBody] OrderMenuItemRequest orderMenuItem)
    {
        var order = await _orderService.GetByIdAndReservationId(orderId, reservationId);
        var menuItem = await _menuItemService.GetByIdAsync(restaurantId, orderMenuItem.MenuItemId);
        if (!await _restaurantService.RestaurantExistsWithIdAsync(restaurantId)
            || !await _reservationService.ReservationsExistsWithRestaurant(restaurantId, reservationId)
            || menuItem is null
            || order is null)
        {
            return NotFound();
        }
        await _orderService.AddOrderMenuItemAsync(order,
            new OrderMenuItem { Item = menuItem, Quantity = orderMenuItem.Quantity });
        return Created(Request.Path, null);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateOrderMenuItem([FromRoute] string restaurantId,
        [FromRoute] string reservationId, [FromRoute] string orderId, [FromBody] OrderMenuItemRequest orderMenuItem)
    {
        var order = await _orderService.GetByIdAndReservationId(orderId, reservationId);
        var menuItem = await _menuItemService.GetByIdAsync(restaurantId, orderMenuItem.MenuItemId);
        if (!await _restaurantService.RestaurantExistsWithIdAsync(restaurantId)
            || !await _reservationService.ReservationsExistsWithRestaurant(restaurantId, reservationId)
            || menuItem is null
            || order is null)
        {
            return NotFound();
        }
        await _orderService.UpdateOrderMenuItemAsync(order,
            new OrderMenuItem { Item = menuItem, Quantity = orderMenuItem.Quantity });
        return NoContent();
    }

    [HttpDelete("{menuItemId}")]
    public async Task<IActionResult> RemoveOrderMenuItem(string restaurantId, string reservationId, string orderId,
        string menuItemId)
    {
        var order = await _orderService.GetByIdAndReservationId(orderId, reservationId);
        if (!await _restaurantService.RestaurantExistsWithIdAsync(restaurantId)
            || !await _reservationService.ReservationsExistsWithRestaurant(restaurantId, reservationId)
            || !await _menuItemService.ExistsWithIdAndRestaurantAsync(restaurantId, menuItemId)
            || order is null)
        {
            return NotFound();
        }
        await _orderService.DeleteOrderMenuItemAsync(order, menuItemId);
        return NoContent();
    }
}