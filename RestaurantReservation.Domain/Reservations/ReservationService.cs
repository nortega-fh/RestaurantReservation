namespace RestaurantReservation.Domain.Reservations;

public class ReservationService : IReservationService
{
    private readonly IReservationAvailabilityChecker _reservationAvailabilityChecker;
    private readonly IReservationRepository _reservationRepository;

    public ReservationService(IReservationRepository reservationRepository,
        IReservationAvailabilityChecker reservationAvailabilityChecker)
    {
        _reservationRepository =
            reservationRepository ?? throw new ArgumentNullException(nameof(reservationRepository));
        _reservationAvailabilityChecker = reservationAvailabilityChecker ??
                                          throw new ArgumentNullException(nameof(reservationAvailabilityChecker));
    }

    public async Task<List<Reservation>> GetAllAsync(string restaurantId, int pageSize, int pageNumber,
        string? customerId)
    {
        return await _reservationRepository.GetAllAsync(restaurantId, pageSize, pageNumber, customerId);
    }

    public async Task<List<Reservation>> GetAllAsync(string restaurantId, string tableId, int pageSize, int pageNumber,
        string? customerId)
    {
        return await _reservationRepository.GetAllAsync(restaurantId, tableId, pageSize, pageNumber, customerId);
    }

    public async Task<Reservation?> GetByIdAsync(string restaurantId, string reservationId)
    {
        return await _reservationRepository.GetByIdAsync(restaurantId, reservationId);
    }

    public async Task<Reservation?> GetByIdAsync(string restaurantId, string tableId, string reservationId)
    {
        return await _reservationRepository.GetByIdAsync(restaurantId, tableId, reservationId);
    }

    public async Task CreateAsync(Reservation reservation)
    {
        if (await _reservationAvailabilityChecker.AreReservationDatesAvailable(reservation))
        {
            await _reservationRepository.CreateAsync(reservation);
        }
    }

    public async Task UpdateAsync(Reservation reservation)
    {
        if (await _reservationAvailabilityChecker.AreReservationDatesAvailable(reservation))
        {
            await _reservationRepository.UpdateAsync(reservation);
        }
    }

    public async Task DeleteAsync(string reservationId)
    {
        await _reservationRepository.DeleteAsync(reservationId);
    }

    public async Task<bool> ReservationsExistsWithRestaurant(string restaurantId, string reservationId)
    {
        return await GetByIdAsync(restaurantId, reservationId) is not null;
    }
}