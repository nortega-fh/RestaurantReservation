using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using RestaurantReservation.Domain.Restaurants;
using RestaurantReservation.Infrastructure.Serializers;

namespace RestaurantReservation.Infrastructure;

public class RestaurantReservationDatabase : IRestaurantReservationDatabase
{
    private readonly IMongoDatabase _database;

    public RestaurantReservationDatabase(IOptions<MongoDbParameters> dbSettings)
    {
        var objectSerializer = new ObjectSerializer(type => ObjectSerializer.AllAllowedTypes(type));
        BsonSerializer.RegisterSerializer(objectSerializer);

        BsonSerializer.RegisterIdGenerator(typeof(string), StringObjectIdGenerator.Instance);

        var camelCaseConvention = new ConventionPack { new CamelCaseElementNameConvention() };
        ConventionRegistry.Register("CamelCase", camelCaseConvention, _ => true);

        BsonClassMap.RegisterClassMap<ScheduleBlock>();
        BsonClassMap.RegisterClassMap<RestaurantSchedule>(classMap =>
        {
            classMap.AutoMap();
            classMap.MapMember(restaurantSchedule => restaurantSchedule.WeeklySchedule)
                .SetSerializer(new WeeklyScheduleSerializer());
        });

        var client = new MongoClient(dbSettings.Value.ConnectionString);
        _database = client.GetDatabase(dbSettings.Value.DbName);
    }

    public IMongoDatabase GetDatabase()
    {
        return _database;
    }
}