namespace RestaurantReservation.API.Contracts.Requests.MenuItems;

public record MenuItemCreate(string Name, string Description, decimal Price);