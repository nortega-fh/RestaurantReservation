namespace RestaurantReservation.API.Contracts.Requests.Customers;

public record CustomerCreate(string Username, string Email, string PhoneNumber);