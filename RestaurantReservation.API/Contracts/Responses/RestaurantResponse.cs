namespace RestaurantReservation.API.Contracts.Responses;

public record RestaurantResponse(
    string Id,
    string Name,
    string Address,
    string PhoneNumber,
    Dictionary<DayOfWeek, string> OpeningHours);