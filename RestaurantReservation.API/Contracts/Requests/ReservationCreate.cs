namespace RestaurantReservation.API.Contracts.Requests;

public record ReservationCreate(DateTime StartDate, DateTime EndDate, int PartySize);