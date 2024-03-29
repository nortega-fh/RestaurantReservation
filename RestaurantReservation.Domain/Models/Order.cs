﻿namespace RestaurantReservation.Domain.Models;

public class Order
{
    public string Id { get; set; } = string.Empty;

    public DateTime OrderDate { get; set; }

    public decimal TotalAmount { get; set; }
}