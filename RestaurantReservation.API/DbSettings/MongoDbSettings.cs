namespace RestaurantReservation.API.Settings;

public class MongoDbSettings
{
    public string ConnectionString { get; set; } = null!;
    public string DbName { get; set; } = null!;
}