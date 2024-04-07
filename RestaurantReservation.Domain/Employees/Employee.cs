using RestaurantReservation.Domain.Users;

namespace RestaurantReservation.Domain.Employees;

public class Employee : User
{
    public string Position { get; set; } = string.Empty;
}