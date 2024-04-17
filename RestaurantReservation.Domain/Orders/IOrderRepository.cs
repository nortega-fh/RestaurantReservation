using RestaurantReservation.Domain.MenuItems;

namespace RestaurantReservation.Domain.Orders;

public interface IOrderRepository
{
    Task<List<Order>> GetAllByReservation(string reservationId, int pageSize, int pageNumber);

    Task<List<MenuItem>> GetAllMenuItemsByReservationAsync(string reservationId, int pageSize, int pageNumber,
        MenuItemOrderableProperties orderBy);

    Task<decimal> GetAverageTotalAmountOrderMenuItemsByEmployeeAsync(string employeeId);

    Task<Order?> GetByIdAndReservationId(string orderId, string reservationId);
    Task Create(Order order);
    Task Update(string orderId, Order order);
    Task Delete(string orderId);
}