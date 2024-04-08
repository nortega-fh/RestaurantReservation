using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.Contracts.Requests;
using RestaurantReservation.API.Contracts.Responses;
using RestaurantReservation.Domain.MenuItems;
using RestaurantReservation.Domain.Restaurants;

namespace RestaurantReservation.API.Controllers;

[ApiController]
[Authorize]
[Route("/api/restaurants/{restaurantId}/menu")]
public class MenuItemController : ControllerBase
{
    private const int DefaultPageSize = 20;
    private const int DefaultPageNumber = 1;
    private readonly IMapper _mapper;
    private readonly IMenuItemService _menuItemService;
    private readonly IRestaurantService _restaurantService;

    public MenuItemController(IMenuItemService menuItemService, IRestaurantService restaurantService, IMapper mapper)
    {
        _menuItemService = menuItemService ?? throw new ArgumentNullException(nameof(menuItemService));
        _restaurantService = restaurantService ?? throw new ArgumentNullException(nameof(restaurantService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet]
    public async Task<IActionResult> GetAllMenuItems([FromRoute] string restaurantId,
        [FromQuery] int pageSize = DefaultPageSize,
        [FromQuery] int pageNumber = DefaultPageNumber)
    {
        var menuItems = _mapper
            .Map<IEnumerable<MenuItemResponse>>(await _menuItemService.GetAllAsync(restaurantId, pageSize, pageNumber))
            .ToList();
        return Ok(new CollectionResponse<MenuItemResponse>
        {
            Metadata = new ResponseMetadata(menuItems.Count, pageSize, pageNumber),
            Items = menuItems
        });
    }

    [HttpGet("{menuItemId}")]
    public async Task<IActionResult> GetMenuItemById(string restaurantId, string menuItemId)
    {
        var item = await _menuItemService.GetByIdAsync(restaurantId, menuItemId);
        return item is null ? NotFound() : Ok(_mapper.Map<MenuItemResponse>(item));
    }

    [HttpPost]
    public async Task<IActionResult> CreateItemById([FromRoute] string restaurantId,
        [FromBody] MenuItemCreate menuItemCreate)
    {
        if (!await _restaurantService.RestaurantExistsWithIdAsync(restaurantId))
        {
            return NotFound();
        }
        var menuItem = _mapper.Map<MenuItem>(menuItemCreate);
        menuItem.RestaurantId = restaurantId;
        await _menuItemService.CreateAsync(menuItem);
        return Created(Request.Path, null);
    }

    [HttpPut("{menuItemId}")]
    public async Task<IActionResult> UpdateMenuItem([FromRoute] string restaurantId, [FromRoute] string menuItemId,
        [FromBody] MenuItemCreate menuItemCreate)
    {
        var menuItem = await _menuItemService.GetByIdAsync(restaurantId, menuItemId);
        if (menuItem is null)
        {
            return NotFound();
        }
        _mapper.Map(menuItemCreate, menuItem);
        await _menuItemService.UpdateAsync(menuItem);
        return NoContent();
    }

    [HttpDelete("{menuItemId}")]
    public async Task<IActionResult> DeleteMenuItem(string restaurantId, string menuItemId)
    {
        if (!await _menuItemService.ExistsWithIdAndRestaurantAsync(restaurantId, menuItemId))
        {
            return NotFound();
        }
        await _menuItemService.DeleteAsync(menuItemId);
        return NoContent();
    }
}