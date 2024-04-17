namespace RestaurantReservation.Domain.Reservations;

public class Reservation
{
    public string Id { get; set; } = string.Empty;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public int PartySize { get; set; }

    public string RestaurantId { get; set; } = string.Empty;

    public string TableId { get; set; } = string.Empty;

    public string CustomerId { get; set; } = string.Empty;
}