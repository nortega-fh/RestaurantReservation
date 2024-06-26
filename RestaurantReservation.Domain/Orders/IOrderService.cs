﻿using RestaurantReservation.Domain.MenuItems;

namespace RestaurantReservation.Domain.Orders;

public interface IOrderService
{
    Task<List<Order>> GetAllOrdersByReservation(string reservationId, int pageSize, int pageNumber);

    Task<List<MenuItem>> GetAllMenuItemsByReservationAsync(string reservationId, int pageSize, int pageNumber,
        MenuItemOrderableProperties orderBy);

    Task<Order?> GetByIdAndReservationId(string orderId, string reservationId);
    Task Create(Order order);
    Task Update(string orderId, Order order);
    Task Delete(string orderId);
    Task<bool> OrderExistsWithReservation(string reservationId, string orderId);
    Task AddOrderMenuItemAsync(Order order, OrderMenuItem orderMenuItem);
    Task UpdateOrderMenuItemAsync(Order order, OrderMenuItem orderMenuItem);
    Task DeleteOrderMenuItemAsync(Order order, string menuItemId);
    Task<decimal> GetAverageOrderAmountByEmployeeAsync(string employeeId);
}