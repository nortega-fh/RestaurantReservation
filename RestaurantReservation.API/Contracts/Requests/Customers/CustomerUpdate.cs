namespace RestaurantReservation.API.Contracts.Requests.Customers;

public record CustomerUpdate(string FirstName, string LastName, string Email, string PhoneNumber);