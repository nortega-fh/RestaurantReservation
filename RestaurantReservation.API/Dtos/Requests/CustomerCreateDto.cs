namespace RestaurantReservation.API.Dtos.Requests;

public record CustomerCreateDto(string FirstName, string LastName, string Email, string PhoneNumber);