namespace RestaurantReservation.Domain.Orders;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;

    public OrderService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
    }

    public async Task<List<Order>> GetAllOrdersByReservation(string reservationId, int pageSize, int pageNumber)
    {
        return await _orderRepository.GetAllByReservation(reservationId, pageSize, pageNumber);
    }

    public async Task<List<Order>> GetAllOrdersByEmployee(string employeeId, int pageSize, int pageNumber)
    {
        return await _orderRepository.GetAllByEmployee(employeeId, pageSize, pageNumber);
    }

    public async Task<Order?> GetByIdAndReservationId(string orderId, string reservationId)
    {
        return await _orderRepository.GetByIdAndReservationId(orderId, reservationId);
    }

    public async Task<Order?> GetByIdAndEmployeeId(string orderId, string employeeId)
    {
        return await _orderRepository.GetByIdAndEmployeeId(orderId, employeeId);
    }

    public async Task Create(Order order)
    {
        await _orderRepository.Create(order);
    }

    public async Task Update(string orderId, Order order)
    {
        await _orderRepository.Update(orderId, order);
    }

    public async Task Delete(string orderId)
    {
        await _orderRepository.Delete(orderId);
    }

    public async Task<bool> OrderExistsWithReservation(string reservationId, string orderId)
    {
        return await _orderRepository.GetByIdAndReservationId(orderId, reservationId) is not null;
    }
}