using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.Contracts.Requests.Tables;
using RestaurantReservation.API.Contracts.Responses.API;
using RestaurantReservation.Domain.Restaurants;
using RestaurantReservation.Domain.Tables;

namespace RestaurantReservation.API.Controllers;

[ApiController]
[Route("/api/restaurants/{restaurantId}/tables")]
[Authorize]
public class TableController : ControllerBase
{
    private const int DefaultPageSize = 20;
    private const int DefaultPageNumber = 1;
    private readonly IMapper _mapper;
    private readonly IRestaurantService _restaurantService;
    private readonly ITableService _tableService;

    public TableController(ITableService tableService, IRestaurantService restaurantService, IMapper mapper)
    {
        _tableService = tableService ?? throw new ArgumentNullException(nameof(tableService));
        _restaurantService = restaurantService ?? throw new ArgumentNullException(nameof(restaurantService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(string restaurantId, int pageSize = DefaultPageSize,
        int pageNumber = DefaultPageNumber)
    {
        if (!await _restaurantService.RestaurantExistsWithIdAsync(restaurantId))
        {
            return NotFound();
        }
        var items = (await _tableService.GetAllAsync(restaurantId, pageSize, pageNumber)).ToList();
        return Ok(new CollectionResponse<Table>
        {
            Metadata = new ResponseMetadata(items.Count, pageSize, pageNumber),
            Items = items
        });
    }

    [HttpGet("{tableId}")]
    public async Task<IActionResult> GetById(string restaurantId, string tableId)
    {
        if (!await _restaurantService.RestaurantExistsWithIdAsync(restaurantId))
        {
            return NotFound();
        }
        var table = await _tableService.GetByIdAsync(tableId);
        return table is not null ? Ok(table) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Create(string restaurantId, TableCreate requestedTable)
    {
        if (!await _restaurantService.RestaurantExistsWithIdAsync(restaurantId))
        {
            return NotFound();
        }
        var table = _mapper.Map<Table>(requestedTable);
        table.RestaurantId = restaurantId;
        await _tableService.CreateAsync(table);
        return Created(Request.Path, null);
    }

    [HttpPut("{tableId}")]
    public async Task<IActionResult> Update(string restaurantId, string tableId, TableCreate updatedTable)
    {
        if (!await _restaurantService.RestaurantExistsWithIdAsync(restaurantId))
        {
            return NotFound();
        }
        if (!await _tableService.ExistsWithIdAsync(tableId))
        {
            return NotFound();
        }
        var table = _mapper.Map<Table>(updatedTable);
        table.Id = tableId;
        table.RestaurantId = restaurantId;
        await _tableService.UpdateAsync(table);
        return NoContent();
    }

    [HttpDelete("{tableId}")]
    public async Task<IActionResult> Delete(string restaurantId, string tableId)
    {
        if (!await _restaurantService.RestaurantExistsWithIdAsync(restaurantId))
        {
            return NotFound();
        }
        if (!await _tableService.ExistsWithIdAsync(tableId))
        {
            return NotFound();
        }
        await _tableService.DeleteAsync(tableId);
        return NoContent();
    }
}