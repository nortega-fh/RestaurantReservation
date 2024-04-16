namespace RestaurantReservation.API.Contracts.Requests.Orders;

public record OrderMenuItemRequest(string MenuItemId, long Quantity);