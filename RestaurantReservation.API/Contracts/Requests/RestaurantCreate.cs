namespace RestaurantReservation.API.Contracts.Requests;

public record RestaurantCreate(
    string Name,
    string Address,
    string PhoneNumber,
    Dictionary<string, string> OpeningHours);