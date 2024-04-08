﻿namespace RestaurantReservation.API.Contracts.Responses;

public class MenuItemResponse
{
    public string Id { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public decimal Price { get; set; }
}