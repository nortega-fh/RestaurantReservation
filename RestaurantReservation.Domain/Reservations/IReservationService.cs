namespace RestaurantReservation.Domain.Reservations;

public interface IReservationService
{
    Task<List<Reservation>> GetAllAsync(string restaurantId, int pageSize, int pageNumber);
    Task<List<Reservation>> GetAllAsync(string restaurantId, string tableId, int pageSize, int pageNumber);
    Task<Reservation?> GetByIdAsync(string restaurantId, string reservationId);
    Task<Reservation?> GetByIdAsync(string restaurantId, string tableId, string reservationId);
    Task CreateAsync(Reservation reservation);
    Task UpdateAsync(Reservation reservation);
    Task DeleteAsync(string reservationId);
}