using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace RestaurantReservation.Infrastructure;

public class RestaurantReservationDatabase : IRestaurantReservationDatabase
{
    private readonly IMongoDatabase _database;

    public RestaurantReservationDatabase(IOptions<MongoDbSettings> dbSettings)
    {
        var client = new MongoClient(dbSettings.Value.ConnectionString);
        _database = client.GetDatabase(dbSettings.Value.DbName);
    }

    public IMongoDatabase GetDatabase()
    {
        return _database;
    }
}