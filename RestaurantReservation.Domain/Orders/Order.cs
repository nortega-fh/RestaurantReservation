namespace RestaurantReservation.Domain.Orders;

public class Order
{
    public string Id { get; set; } = string.Empty;

    public string ReservationId { get; set; } = string.Empty;

    public string EmployeeId { get; set; } = string.Empty;

    public DateTime OrderDate { get; set; }

    public decimal TotalAmount { get; set; }

    public IEnumerable<OrderMenuItem> Items { get; set; } = new List<OrderMenuItem>();
}