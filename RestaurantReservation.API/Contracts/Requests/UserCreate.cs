namespace RestaurantReservation.API.Contracts.Requests;

public record UserCreate(string Username, string Password, string FirstName, string LastName);