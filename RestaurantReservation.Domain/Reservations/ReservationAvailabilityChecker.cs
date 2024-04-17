using RestaurantReservation.Domain.Restaurants;
using RestaurantReservation.Domain.Tables;

namespace RestaurantReservation.Domain.Reservations;

public class ReservationAvailabilityChecker : IReservationAvailabilityChecker
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IRestaurantService _restaurantService;
    private readonly ITableService _tableService;

    public ReservationAvailabilityChecker(IReservationRepository reservationRepository,
        IRestaurantService restaurantService, ITableService tableService)
    {
        _reservationRepository =
            reservationRepository ?? throw new ArgumentNullException(nameof(reservationRepository));
        _restaurantService = restaurantService ?? throw new ArgumentNullException(nameof(restaurantService));
        _tableService = tableService ?? throw new ArgumentNullException(nameof(tableService));
    }

    public async Task<bool> AreReservationDatesAvailable(Reservation reservation)
    {
        var overlappedReservations =
            (await _reservationRepository.GetByTableAndBetweenDates(reservation.TableId, reservation.StartDate,
                reservation.EndDate)).Where(r => !r.Id.Equals(reservation.Id)).ToList();
        if (overlappedReservations.Count > 0)
        {
            throw new BadHttpRequestException("The requested table is not available in these dates.");
        }
        var isClosedAtReservationStart =
            !await _restaurantService.IsRestaurantAvailableOnDateAsync(reservation.RestaurantId, reservation.StartDate);
        var isClosedAtReservationEnd =
            !await _restaurantService.IsRestaurantAvailableOnDateAsync(reservation.RestaurantId, reservation.EndDate);
        var closedDate = new DateTime();
        if (isClosedAtReservationStart)
        {
            closedDate = reservation.StartDate;
        }
        else if (isClosedAtReservationEnd)
        {
            closedDate = reservation.EndDate;
        }
        if (isClosedAtReservationStart || isClosedAtReservationEnd)
        {
            throw new BadHttpRequestException($"The restaurant is not open at date {closedDate}");
        }
        var tableCapacityIsLessThanPartySize =
            ((await _tableService.GetByIdAsync(reservation.TableId))?.Capacity ?? -1) < reservation.PartySize;
        if (tableCapacityIsLessThanPartySize)
        {
            throw new BadHttpRequestException("The table doesn't have the capacity for the party size");
        }
        return true;
    }
}