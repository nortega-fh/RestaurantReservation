namespace RestaurantReservation.API.Contracts.Requests;

public record MenuItemCreate(string Name, string Description, decimal Price);