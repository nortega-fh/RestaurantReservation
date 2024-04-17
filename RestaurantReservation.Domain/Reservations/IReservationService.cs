using RestaurantReservation.Domain.MenuItems;

namespace RestaurantReservation.Domain.Reservations;

public interface IReservationService
{
    Task<List<Reservation>> GetAllAsync(string restaurantId, int pageSize, int pageNumber, string? customerId);

    Task<List<Reservation>> GetAllAsync(string restaurantId, string tableId, int pageSize, int pageNumber,
        string? customerId);

    Task<Reservation?> GetByIdAsync(string restaurantId, string reservationId);
    Task<Reservation?> GetByIdAsync(string restaurantId, string tableId, string reservationId);

    Task<List<MenuItem>> GetReservationMenuItemsAsync(string reservationId, int pageSize, int pageNumber,
        MenuItemOrderableProperties orderBy);

    Task CreateAsync(Reservation reservation);
    Task UpdateAsync(Reservation reservation);
    Task DeleteAsync(string reservationId);
    Task<bool> ReservationsExistsWithRestaurant(string restaurantId, string reservationId);
}