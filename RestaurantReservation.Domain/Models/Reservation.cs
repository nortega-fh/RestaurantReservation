namespace RestaurantReservation.Domain.Models;

public class Reservation
{
    public string Id { get; set; } = string.Empty;

    public DateTime ReservationDate { get; set; }

    public int PartySize { get; set; }
}