namespace RestaurantReservation.API.Contracts.Requests;

public record CustomerUpdate(string FirstName, string LastName, string Email, string PhoneNumber);