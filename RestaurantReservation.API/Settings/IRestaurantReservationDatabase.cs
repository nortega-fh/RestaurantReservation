using MongoDB.Driver;

namespace RestaurantReservation.API.Settings;

public interface IRestaurantReservationDatabase
{
    IMongoDatabase GetDatabase();
}