using MongoDB.Driver;
using MongoDB.Driver.Linq;
using RestaurantReservation.Domain.MenuItems;
using RestaurantReservation.Domain.Orders;

namespace RestaurantReservation.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly IMongoCollection<Order> _collection;

    public OrderRepository(IRestaurantReservationDatabase database)
    {
        _collection = database.GetDatabase().GetCollection<Order>("Orders");
    }

    public async Task<List<Order>> GetAllByReservation(string reservationId, int pageSize, int pageNumber)
    {
        return await _collection.AsQueryable()
            .Where(order => reservationId.Equals(order.ReservationId))
            .Skip(pageNumber * pageSize - pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<List<MenuItem>> GetAllMenuItemsByReservationAsync(string reservationId, int pageSize,
        int pageNumber, MenuItemOrderableProperties orderBy)
    {
        var query = _collection.AsQueryable()
            .Where(order => reservationId.Equals(order.ReservationId))
            .SelectMany(order => order.Items)
            .Select(orderMenuItem => orderMenuItem.Item);
        query = orderBy is MenuItemOrderableProperties.Name
            ? query.OrderBy(item => item.Name)
            : query.OrderBy(item => item.Price);
        return await query
            .Skip(pageNumber * pageSize - pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<List<Order>> GetAllByEmployee(string employeeId, int pageSize, int pageNumber)
    {
        return await _collection.AsQueryable()
            .Where(order => employeeId.Equals(order.EmployeeId))
            .Skip(pageNumber * pageSize - pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<Order?> GetByIdAndReservationId(string orderId, string reservationId)
    {
        return await _collection.AsQueryable()
            .Where(order => orderId.Equals(order.Id) && reservationId.Equals(order.ReservationId))
            .SingleOrDefaultAsync();
    }

    public async Task<Order?> GetByIdAndEmployeeId(string orderId, string employeeId)
    {
        return await _collection.AsQueryable()
            .Where(order => orderId.Equals(order.Id) && employeeId.Equals(order.EmployeeId))
            .SingleOrDefaultAsync();
    }

    public async Task Create(Order order)
    {
        await _collection.InsertOneAsync(order);
    }

    public async Task Update(string orderId, Order order)
    {
        var filterById = Builders<Order>.Filter.Eq(o => o.Id, orderId);
        await _collection.FindOneAndReplaceAsync(filterById, order);
    }

    public async Task Delete(string orderId)
    {
        var filterById = Builders<Order>.Filter.Eq(o => o.Id, orderId);
        await _collection.FindOneAndDeleteAsync(filterById);
    }
}