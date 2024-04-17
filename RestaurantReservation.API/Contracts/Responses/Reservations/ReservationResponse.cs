namespace RestaurantReservation.API.Contracts.Responses.Reservations;

public class ReservationResponse
{
    public string Id { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string TableId { get; set; } = string.Empty;
    public string CustomerId { get; set; } = string.Empty;
}