namespace RestaurantReservation.Domain.Reservations;

public interface IReservationAvailabilityChecker
{
    Task<bool> AreReservationDatesAvailable(Reservation reservation);
}