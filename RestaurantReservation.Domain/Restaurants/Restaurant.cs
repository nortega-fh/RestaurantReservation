namespace RestaurantReservation.Domain.Restaurants;

public class Restaurant
{
    public string Id { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public RestaurantSchedule Schedule { get; set; } = new();
}