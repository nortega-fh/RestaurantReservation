using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RestaurantReservation.API.Entities;

public class Restaurant
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("name")]
    public string Name { get; set; }

    [BsonElement("address")]
    public string Address { get; set; }

    [BsonElement("phone_number")]
    public string PhoneNumber { get; set; }

    [BsonElement("opening_hours")]
    public Dictionary<DayOfWeek, string> OpeningHours { get; set; }
}