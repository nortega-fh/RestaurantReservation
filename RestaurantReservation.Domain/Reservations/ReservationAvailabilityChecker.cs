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
        if ((await _reservationRepository.GetByRestaurantAndBetweenDates(reservation.RestaurantId,
                reservation.StartDate, reservation.EndDate)).Count > 0)
        {
            throw new BadHttpRequestException("There are other reservations in the dates requested.");
        }
        if (!await _restaurantService.IsRestaurantAvailableOnDateAsync(reservation.RestaurantId,
                reservation.StartDate))
        {
            throw new BadHttpRequestException($"The restaurant is not open at date {reservation.StartDate}");
        }
        if (((await _tableService.GetByIdAsync(reservation.TableId))?.Capacity ?? -1) < reservation.PartySize)
        {
            throw new BadHttpRequestException("The table doesn't have the capacity for the party size");
        }
        return true;
    }
}