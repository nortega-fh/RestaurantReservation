﻿namespace RestaurantReservation.Domain.MenuItems;

public class MenuItem
{
    public string Id { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public string RestaurantId { get; set; } = string.Empty;
}