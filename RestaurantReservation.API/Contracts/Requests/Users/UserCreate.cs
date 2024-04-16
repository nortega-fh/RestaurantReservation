namespace RestaurantReservation.API.Contracts.Requests.Users;

public record UserCreate(string Username, string Password, string FirstName, string LastName);