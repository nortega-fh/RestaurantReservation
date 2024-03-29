namespace RestaurantReservation.Domain.Models;

public class Customer : User
{
    public string Email { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;
}