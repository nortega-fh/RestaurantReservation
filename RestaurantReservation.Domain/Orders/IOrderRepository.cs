using RestaurantReservation.Domain.MenuItems;

namespace RestaurantReservation.Domain.Orders;

public interface IOrderRepository
{
    Task<List<Order>> GetAllByReservation(string reservationId, int pageSize, int pageNumber);

    Task<List<MenuItem>> GetAllMenuItemsByReservationAsync(string reservationId, int pageSize, int pageNumber,
        MenuItemOrderableProperties orderBy);

    Task<List<Order>> GetAllByEmployee(string employeeId, int pageSize, int pageNumber);
    Task<Order?> GetByIdAndReservationId(string orderId, string reservationId);
    Task<Order?> GetByIdAndEmployeeId(string orderId, string employeeId);
    Task Create(Order order);
    Task Update(string orderId, Order order);
    Task Delete(string orderId);
}