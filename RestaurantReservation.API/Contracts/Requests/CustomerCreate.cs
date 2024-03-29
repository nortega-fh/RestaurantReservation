namespace RestaurantReservation.API.Contracts.Requests;

public record CustomerCreate(string FirstName, string LastName, string Email, string PhoneNumber);