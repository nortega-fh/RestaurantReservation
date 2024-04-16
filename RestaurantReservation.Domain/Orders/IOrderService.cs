namespace RestaurantReservation.Domain.Orders;

public interface IOrderService
{
    Task<List<Order>> GetAllOrdersByReservation(string reservationId, int pageSize, int pageNumber);
    Task<List<Order>> GetAllOrdersByEmployee(string employeeId, int pageSize, int pageNumber);
    Task<Order?> GetByIdAndReservationId(string orderId, string reservationId);
    Task<Order?> GetByIdAndEmployeeId(string orderId, string employeeId);
    Task Create(Order order);
    Task Update(string orderId, Order order);
    Task Delete(string orderId);
    Task<bool> OrderExistsWithReservation(string reservationId, string orderId);
}