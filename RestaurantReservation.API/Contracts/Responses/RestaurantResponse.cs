namespace RestaurantReservation.API.Contracts.Responses;

public class RestaurantResponse
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public Dictionary<DayOfWeek, string> OpeningHours { get; set; } = new();
}