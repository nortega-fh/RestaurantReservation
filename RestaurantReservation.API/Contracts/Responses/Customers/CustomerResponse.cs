﻿namespace RestaurantReservation.API.Contracts.Responses.Customers;

public class CustomerResponse
{
    public string Id { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
}