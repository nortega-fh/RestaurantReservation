namespace RestaurantReservation.API.Contracts.Responses;

public record CustomerResponse(string Id, string Email, string Phone, string FirstName, string LastName);