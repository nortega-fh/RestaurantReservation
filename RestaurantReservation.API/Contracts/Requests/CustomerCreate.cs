namespace RestaurantReservation.API.Contracts.Requests;

public record CustomerCreate(string Username, string FirstName, string LastName, string Email, string PhoneNumber);