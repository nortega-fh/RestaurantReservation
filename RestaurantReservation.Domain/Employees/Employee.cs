using RestaurantReservation.Domain.Users;

namespace RestaurantReservation.Domain.Employees;

public class Employee : User
{
    public string RestaurantId { get; set; } = string.Empty;
    public string Position { get; set; } = string.Empty;
}