namespace RestaurantReservation.API.Contracts.Responses;

public class OrderReservationResponse
{
    public string Id { get; set; } = null!;
    public string EmployeeId { get; set; } = null!;
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public IEnumerable<MenuItemResponse> Items { get; set; } = null!;
}