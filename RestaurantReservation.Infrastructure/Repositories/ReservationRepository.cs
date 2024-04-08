using System.Linq.Expressions;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using RestaurantReservation.Domain.Reservations;

namespace RestaurantReservation.Infrastructure.Repositories;

public class ReservationRepository : IReservationRepository
{
    private readonly IMongoCollection<Reservation> _collection;

    public ReservationRepository(IRestaurantReservationDatabase database)
    {
        _collection = database.GetDatabase().GetCollection<Reservation>("Reservations");
    }

    public async Task<List<Reservation>> GetAllAsync(string restaurantId, int pageSize, int pageNumber)
    {
        return await GetReservationPageListByCondition(reservation => reservation.RestaurantId.Equals(restaurantId),
            pageSize, pageNumber);
    }

    public async Task<List<Reservation>> GetAllAsync(string restaurantId, string tableId, int pageSize, int pageNumber)
    {
        return await GetReservationPageListByCondition(
            reservation => restaurantId.Equals(reservation.RestaurantId) && tableId.Equals(reservation.TableId),
            pageSize, pageNumber);
    }

    public async Task<List<Reservation>> GetByRestaurantAndBetweenDates(string restaurantId, DateTime startDate,
        DateTime endDate)
    {
        return await _collection.AsQueryable()
            .Where(reservation => reservation.RestaurantId.Equals(restaurantId) &&
                                  ((reservation.StartDate >= startDate &&
                                    reservation.StartDate <= endDate) ||
                                   (reservation.EndDate >= startDate &&
                                    reservation.EndDate <= endDate)))
            .ToListAsync();
    }

    public async Task<Reservation?> GetByIdAsync(string restaurantId, string reservationId)
    {
        return await GetReservationByCondition(reservation =>
            restaurantId.Equals(reservation.RestaurantId) && reservationId.Equals(reservation.Id));
    }

    public async Task<Reservation?> GetByIdAsync(string restaurantId, string tableId, string reservationId)
    {
        return await GetReservationByCondition(reservation =>
            restaurantId.Equals(reservation.RestaurantId) && tableId.Equals(reservation.TableId) &&
            reservationId.Equals(reservation.Id));
    }

    public async Task CreateAsync(Reservation reservation)
    {
        await _collection.InsertOneAsync(reservation);
    }

    public async Task UpdateAsync(Reservation reservation)
    {
        var findById = Builders<Reservation>.Filter.Eq(r => r.Id, reservation.Id);
        await _collection.FindOneAndReplaceAsync(findById, reservation);
    }

    public async Task DeleteAsync(string reservationId)
    {
        var findById = Builders<Reservation>.Filter.Eq(r => r.Id, reservationId);
        await _collection.FindOneAndDeleteAsync(findById);
    }

    private async Task<List<Reservation>> GetReservationPageListByCondition(
        Expression<Func<Reservation, bool>> condition,
        int pageSize, int pageNumber)
    {
        return await _collection.AsQueryable()
            .Where(condition)
            .Skip(pageNumber * pageSize - pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    private async Task<Reservation?> GetReservationByCondition(Expression<Func<Reservation, bool>> condition)
    {
        return await _collection.AsQueryable()
            .Where(condition)
            .FirstOrDefaultAsync();
    }
}