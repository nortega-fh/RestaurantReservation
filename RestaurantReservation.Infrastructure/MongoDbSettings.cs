namespace RestaurantReservation.Infrastructure;

public class MongoDbSettings
{
    public string ConnectionString { get; set; } = null!;
    public string DbName { get; set; } = null!;
}