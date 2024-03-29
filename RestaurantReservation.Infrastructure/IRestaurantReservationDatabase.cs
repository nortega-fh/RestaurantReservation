using MongoDB.Driver;

namespace RestaurantReservation.Infrastructure;

public interface IRestaurantReservationDatabase
{
    IMongoDatabase GetDatabase();
}