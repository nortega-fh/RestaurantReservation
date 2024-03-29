namespace RestaurantReservation.Domain.Models;

public class Employee : User
{
    public string Position { get; set; } = string.Empty;
}