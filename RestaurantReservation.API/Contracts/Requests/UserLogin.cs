﻿namespace RestaurantReservation.API.Contracts.Requests;

public class UserLogin
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}