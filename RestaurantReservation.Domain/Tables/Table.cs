namespace RestaurantReservation.Domain.Tables;

public class Table
{
    public string Id { get; set; } = string.Empty;

    public int Capacity { get; set; }

    public string RestaurantId { get; set; } = null!;
}