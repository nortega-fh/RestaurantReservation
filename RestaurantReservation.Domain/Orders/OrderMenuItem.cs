using RestaurantReservation.Domain.MenuItems;

namespace RestaurantReservation.Domain.Orders;

public class OrderMenuItem
{
    public MenuItem Item { get; set; } = null!;
    public long Quantity { get; set; }
}