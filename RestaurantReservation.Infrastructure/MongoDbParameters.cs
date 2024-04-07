namespace RestaurantReservation.Infrastructure;

public class MongoDbParameters
{
    public string ConnectionString { get; set; } = null!;
    public string DbName { get; set; } = null!;
}