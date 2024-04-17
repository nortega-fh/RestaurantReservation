namespace RestaurantReservation.API.Contracts.Requests.Reservations;

public record ReservationCreate(DateTime StartDate, DateTime EndDate, int PartySize, string CustomerId);