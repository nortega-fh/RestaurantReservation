namespace RestaurantReservation.API.Contracts.Responses.Orders;

public class OrderReservationResponse
{
    public string Id { get; set; } = null!;
    public string EmployeeId { get; set; } = null!;
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
}