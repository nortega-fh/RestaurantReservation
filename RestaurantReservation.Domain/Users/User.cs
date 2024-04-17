﻿namespace RestaurantReservation.Domain.Users;

public class User
{
    public string Id { get; set; } = null!;

    public string Username { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;
}